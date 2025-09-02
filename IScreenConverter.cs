using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontToLCD
{
    using System.Drawing;

    public interface IScreenConverter
    {
        /// <summary>
        /// 將點陣矩陣轉換為目標螢幕的 HEX 字串。
        /// </summary>
        /// <param name="matrix">0/1 的二維矩陣。</param>
        /// <param name="foregroundColor">前景顏色 (對單色螢幕可能無效)。</param>
        /// <param name="backgroundColor">背景顏色 (對單色螢幕可能無效)。</param>
        /// <returns>格式化後的 HEX 字串。</returns>
        string Convert(int[,] matrix, Color foregroundColor, Color backgroundColor);

    }
}
