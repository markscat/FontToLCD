// Ili9225Converter.cs
using System.Text;
using System.Drawing;
namespace FontToLCD
{
    /// <summary>
    /// 一個專門的轉換器，負責將 0/1 的二維矩陣轉換為適用於 ILI9225 彩色螢幕的 HEX 字串。
    /// 這個類別實作了 IScreenConverter 介面，代表它是一個標準的「螢幕轉換器」。
    /// </summary>
    public class Ili9225Converter : IScreenConverter
    {

        /// <summary>
        /// 執行轉換的核心方法。
        /// 它會將代表「前景」的 1 和代表「背景」的 0，分別對應到使用者指定的兩種顏色。
        /// </summary>
        /// <param name="matrix">從主程式傳入的 0/1 二維陣列。</param>
        /// <param name="foregroundColor">使用者定義的前景顏色 (對應矩陣中的 1)。</param>
        /// <param name="backgroundColor">使用者定義的背景顏色 (對應矩陣中的 0)。</param>
        /// <returns>一個 C/C++ 風格的 uint16_t 陣列字串，每個元素代表一個像素的 RGB565 顏色。</returns>
        /// 
        public string Convert(int[,] matrix, Color foregroundColor, Color backgroundColor)
        {


            // --- 步驟 1: 顏色格式轉換 ---
            // ILI9225 等 TFT 螢幕通常使用 RGB565 格式，每個像素用 16 位元 (2 個位元組) 表示顏色。
            // C# 的 Color 物件是 24 位元 (RGB888)，所以需要進行轉換。
            // 這個轉換在迴圈開始前完成，可以避免在迴圈內重複計算，提高效率。

            // 將前景顏色從 24-bit RGB888 轉換為 16-bit RGB565。
            // R (8位) -> R (5位): foregroundColor.R >> 3
            // G (8位) -> G (6位): foregroundColor.G >> 2
            // B (8位) -> B (5位): foregroundColor.B >> 3
            // 然後透過位元運算，將這三部分打包成一個 16 位元的 ushort。
            ushort fgColor565 = (ushort)(((foregroundColor.R >> 3) << 11) | ((foregroundColor.G >> 2) << 5) | (foregroundColor.B >> 3));
            // 對背景顏色執行相同的轉換操作。
            ushort bgColor565 = (ushort)(((backgroundColor.R >> 3) << 11) | ((backgroundColor.G >> 2) << 5) | (backgroundColor.B >> 3));

            // 使用 StringBuilder 來高效地建立最終的輸出字串。
            StringBuilder sb = new StringBuilder();

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // 陣列的開頭
            string dataType = "const uint8_t";
            string arrayName = $"myChar_{cols}x{rows}";
            int arraySize = (cols + 7) / 8 * rows;
            sb.AppendLine($"// Bitmap Font Data for a {cols}x{rows} character");
            sb.AppendLine($"{dataType} {arrayName}[{arraySize}] = {{"); // <-- 修改過的開頭

            // --- 步驟 2: 遍歷矩陣並產生 HEX 字串 ---
            // 外層迴圈：逐行處理 (y 座標)，這對應了「水平掃描」模式。
            for (int y = 0; y < rows; y++)
            {
                sb.Append("  ");
                // 內層迴圈：逐一處理該行中的每個像素 (x 座標)。
                for (int x = 0; x < cols; x++)
                {
                    // 檢查當前像素是前景 (1) 還是背景 (0)。
                    if (matrix[y, x] == 1)
                    {
                        // 如果是 1，就附加前景顏色的 HEX 值。
                        // "X4" 格式化指令表示：X=大寫十六進位，4=總是顯示四位數 (例如 07E0)。
                        sb.Append($"0x{fgColor565:X4}, ");
                    }
                    else
                    {
                        // 如果是 0，就附加背景顏色的 HEX 值。
                        sb.Append($"0x{bgColor565:X4}, ");
                    }
                }
                sb.AppendLine(); // 處理完一行後換行。
            }
            sb.AppendLine("};"); // 陣列的結尾

            // 回傳最終組合好的完整字串。

            return sb.ToString();
        }
    }
}