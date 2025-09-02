// HorizontalMonoConverter.cs
using System.Text; // 需要 StringBuilder
using System.Drawing; // 需要 Color
namespace FontToLCD
{
    public class HorizontalMonoConverter : IScreenConverter
    {

        /// <summary>
        /// 將 0/1 的二維矩陣轉換為 C/C++ 風格的十六進位陣列字串。
        /// 這種格式適用於「水平掃描」或「行主序 (Row-Major)」的顯示器。
        /// </summary>
        /// <param name="matrix">包含 0 和 1 的二維整數陣列，代表像素點陣。</param>
        /// <returns>格式化後的十六進位陣列字串。</returns>
        /// <remarks>
        /// --- 格式規格 ---
        /// 1. 掃描方向：由上到下 (Row by Row)，由左到右 (Pixel by Pixel)。
        /// 2. 像素打包：每 8 個像素打包成一個位元組 (Byte)。
        /// 3. 位元順序 (Bit Order)：【重要】一行中，最左邊的像素是最高有效位 (MSB-First)。
        ///    例如，像素序列 [1,0,0,0,0,0,0,0] 會被轉換成 0b10000000，也就是 0x80。
        /// 4. 結尾處理：如果一行像素的寬度不是 8 的倍數，最後一個位元組的剩餘位元將會向左對齊，右邊補 0。
        ///    例如，一行有 10 個像素，前 8 個打包成一個 Byte，後 2 個會打包成 `[P8, P9, 0, 0, 0, 0, 0, 0]`。
        /// 
        /// --- 如何修改 ---
        /// - 若要改為「垂直掃描」(適用於 SH1106/SSD1306)，需要將內外兩層 for 迴圈的 y 和 x 對調，並重新設計位元打包邏輯。
        /// - 若要改為「最低有效位在前 (LSB-First)」，需要修改位元打包的公式，
        /// 例如改為 `value = value | (matrix[y, x] << bitCount);` 並在打包滿 8 位時反轉位元順序，或直接從右向左累加。
        /// </remarks>
        /// 
        public string Convert(int[,] matrix, Color foregroundColor, Color backgroundColor)
        {
            // ▼▼▼ 把您原本 MatrixToHexString 的完整邏輯貼在這裡 ▼▼▼
            StringBuilder sb = new StringBuilder();
            int rows = matrix.GetLength(0);// 獲取矩陣的高度 (總共有幾行)
            int cols = matrix.GetLength(1);// 獲取矩陣的寬度 (總共有幾欄)

            sb.AppendLine("{");

            // --- 外層迴圈：逐行處理 ---
            // 這個迴圈從第一行 (y=0) 開始，一直處理到最後一行。
            for (int y = 0; y < rows; y++)
            {
                sb.Append("  ");    // 在每一行的開頭加上縮排，使格式更美觀
                int value = 0;      // 用於儲存當前正在打包的 8 位元位元組的值
                int bitCount = 0;   // 計數器，計算當前位元組已經打包了幾個位元

                // --- 內層迴圈：處理一行中的每一個像素 ---
                // 這個迴圈從最左邊的像素 (x=0) 開始，處理到最右邊。
                for (int x = 0; x < cols; x++)
                {

                    // --- 核心位元打包邏輯 (MSB-First) ---
                    // 1. `(Value << 1)`: 將目前的值向左移動一位，為新的位元騰出空間 (最低位 D0 會變成 0)。
                    // 2. `| matrix[y, x]`: 將新的像素值 (0 或 1) 透過「或」運算，放入剛騰出的最低位。
                    // 這個操作重複 8 次後，第一個進來的像素就會被推到最高位 (D7)。
                    value = (value << 1) | matrix[y, x];

                    bitCount++; // 每處理一個像素，計數器加一

                    // --- 檢查是否已打包滿一個位元組 ---
                    if (bitCount == 8)
                    {
                        // 如果已經打包了 8 個位元，就將其轉換為十六進位字串並附加
                        // "X2" 格式化指令表示：X=大寫十六進位，2=總是顯示兩位數 (例如 7 會變成 07)
                        sb.Append($"0x{value:X2}, ");

                        // 重設變數，準備打包下一個位元組
                        value = 0;
                        bitCount = 0;
                    }
                }
                if (bitCount > 0)
                {
                    // 將剩餘的位元向左移動，直到填滿一個 8 位的位元組。
                    // 這樣可以確保剩餘的像素位於高位，而低位會自動補 0。
                    // 例如，若剩餘 3 個位元 (bitCount=3)，則需要左移 8-3=5 次。
                    value <<= (8 - bitCount);
                    sb.Append($"0x{value:X2}, ");
                }
                // 處理完一行後，換行以增加可讀性
                sb.AppendLine();
            }
            sb.AppendLine("};");
            return sb.ToString();
            // ▲▲▲ 貼到這裡結束 ▲▲▲
        }
    }

  
}
