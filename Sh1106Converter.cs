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

        sb.AppendLine("{");
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