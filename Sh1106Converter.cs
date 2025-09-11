// Sh1106Converter.cs
using System.Text;
using System.Drawing;
namespace FontToLCD
{
    public class Sh1106Converter : IScreenConverter
{
        public string Convert(int[,] matrix, Color foregroundColor, Color backgroundColor)
        {
            // ▼▼▼ 把您原本 MatrixToHexString_For_SH1106 的完整邏輯貼在這裡 ▼▼▼
            StringBuilder sb = new StringBuilder();
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int numPages = (rows + 7) / 8;

            string dataType = "const uint8_t";
            string arrayName = $"myChar_{cols}x{rows}_SH1106"; // 名字裡可以加上格式
            int arraySize = cols * ((rows + 7) / 8); // 垂直掃描的總大小計算方式


            sb.AppendLine($" /** GNU GENERAL PUBLIC LICENSE\r\n" +
            $"* Version 3, 29 June 2007\r\n " +
            $"* Copyright (C) [2025] [Ethan]\r\n * " +
            $"* 本程式是一個自由軟體：您可以依照 **GNU 通用公共授權條款（GPL）** 發佈和/或修改，\r\n " +
            $"* GPL 版本 3 或（依您選擇）任何更新版本。\r\n " +
            $"* 本程式的發佈目的是希望它對您有幫助，但 **不提供任何擔保**，" +
            $"* 甚至不包含適銷性或特定用途適用性的默示擔保。\r\n " +
            $"* 請參閱 **GNU 通用公共授權條款** 以獲取更多詳細資訊。\r\n " +
            $"* 您應當已經收到一份 **GNU 通用公共授權條款** 副本。\r\n " +
            $"* 如果沒有，請參閱 <https://www.gnu.org/licenses/gpl-3.0.html>。\r\n" +
            $"* \r\n" +
            $"* 本程式主要生成單一字元的陣列,或是自訂單一字元的字型\r\n" +
            $"* 生成的陣列依照Sh1106所需要數值的陣列，\r\n" +
            $"* To do:\r\n" +
            $"* 生成所有的Ascii 陣列\r\n" +
            $"* 生成多語言字型檔\r\n" +
            $"*/");

            sb.AppendLine($"// Vertical Scan (SH1106/SSD1306) Font Data for a {cols}x{rows} character");
            sb.AppendLine($"{dataType} {arrayName}[{arraySize}] = {{");

            sb.Append("  ");
        for (int p = 0; p < numPages; p++)
        {
            for (int x = 0; x < cols; x++)
            {
                int value = 0;
                for (int bit = 0; bit < 8; bit++)
                {
                    int y = (p * 8) + bit;
                    if (y < rows)
                    {
                        if (matrix[y, x] == 1)
                        {
                            value |= (1 << bit);
                        }
                    }
                }
                sb.Append($"0x{value:X2}, ");
            }
            sb.AppendLine($" // Page {p}");
        }
        sb.AppendLine("};");
        return sb.ToString();
        // ▲▲▲ 貼到這裡結束 ▲▲▲
    }
}
}