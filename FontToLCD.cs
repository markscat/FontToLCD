
using System.Text;
using System.Windows.Forms;
using System;
using System.Drawing;
//using System.Text;
using System.Windows.Forms;

namespace FontToLCD
    {
        public partial class FontToLCD : Form
        {
            public FontToLCD()
            {
                InitializeComponent();
                InitForm();
            }

            private void InitForm()
            {
                // 初始化字型 ComboBox
                foreach (FontFamily ff in FontFamily.Families)
                FontType.Items.Add(ff.Name);
                FontType.SelectedIndex = 0;

                // 初始化大小 ComboBox
            //    LedFont.Items.AddRange(new object[] { "8", "16", "32" });
            //    LedFont.SelectedIndex = 0; // 預設 8
                FontSize.Items.AddRange(new object[] { "8", "12", "16", "24", "32" });
                FontSize.SelectedIndex = 0;
            // 按鈕事件
                button1.Click += Button1_Click;

            // 預設文字
                Imput.Text = "a";
            // 初始化目標螢幕類型 ComboBox
            HEX_Type.Items.AddRange(new object[] {
                "通用水平掃描 (單色)",
                "OLED SH1106/SSD1306 (垂直)",
                "TFT ILI9225 (彩色 RGB565)"
            });
            HEX_Type.SelectedIndex = 0; // 預設選擇第一個選項
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Imput.Text)) return;

            string text = Imput.Text;
            string fontName = FontType.SelectedItem?.ToString() ?? "Arial";
            //int fontSize = int.TryParse(FontSize.SelectedItem?.ToString(), out int fs) ? fs : 16;
            float fontSize = float.TryParse(FontSize.SelectedItem?.ToString(), out float fs) ? fs : 16;

            int[,] matrix; // 用來儲存最終的 0/1 矩陣


            // 畫字
            using (Bitmap bmp = DrawCharToBitmap(text.Substring(0, 1), fontName, fontSize))
            {
                // 轉矩陣
                matrix = BitmapToMatrix(bmp);


                // --- 步驟 2: 根據 ComboBox 的選擇，建立對應的轉換器 ---
                // 這就是「工廠模式」的簡單實現，根據訂單（使用者選擇）生產對應的工具。
                IScreenConverter? converter = null; // 宣告一個介面變數，它可以持有任何一種轉換器
                string selectedScreen = HEX_Type.SelectedItem?.ToString() ?? "";


                switch (selectedScreen)
                {
                    case "通用水平掃描 (單色)":
                        converter = new HorizontalMonoConverter();
                        break;
                    case "OLED SH1106/SSD1306 (垂直)":
                        converter = new Sh1106Converter();
                        break;
                    case "TFT ILI9225 (彩色 RGB565)":
                        converter = new Ili9225Converter();
                        break;
                    default:
                        // 如果使用者沒有選擇，給出提示並退出
                        MessageBox.Show("請選擇一個有效的 HEX 輸出類型！");
                        return;
                }


                // --- 步驟 3: 執行轉換並更新 UI ---
                // 獲取顏色。即使是單色轉換器，我們也傳入顏色，只是它不會使用而已。
                // 這裡我們用黑色和白色作為預設值。
                // 如果您加入了顏色選擇器，就從那裡獲取顏色。
                Color foregroundColor = Color.Black;
                Color backgroundColor = Color.White;

                // 這就是整個架構最優雅的地方！
                // 無論 converter 到底是哪種轉換器，我們都用完全相同的方式呼叫它。
                // 主程式不需要知道轉換的任何細節。
                Hex.Text = converter.Convert(matrix, foregroundColor, backgroundColor);

                // 預覽（放大顯示）
                BitView.Image = DrawMatrixPreview(matrix, 20);
            }
        }

        private Bitmap DrawCharToBitmap(string text, string fontName, float fontSize)
        {
            // 步驟 1: 先用 MeasureString 產生一個「夠大」的、包含空白的初始點陣圖
            Bitmap initialBmp;
            using (Font font = new Font(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Point))
            {
                using (Bitmap tempBmp = new Bitmap(1, 1))
                using (Graphics g = Graphics.FromImage(tempBmp))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                    SizeF size = g.MeasureString(text, font);
                    int bmpWidth = (int)Math.Ceiling(size.Width);
                    int bmpHeight = (int)Math.Ceiling(size.Height);

                    if (bmpWidth <= 0 || bmpHeight <= 0) return new Bitmap(1, 1);

                    // 建立初始點陣圖
                    initialBmp = new Bitmap(bmpWidth, bmpHeight);
                    using (Graphics initialG = Graphics.FromImage(initialBmp))
                    {
                        initialG.Clear(Color.White);
                        initialG.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                        initialG.DrawString(text, font, Brushes.Black, 0, 0);
                    }
                }
            }

            // 步驟 2: 掃描這個初始點陣圖，找出字元筆劃的「真正」視覺邊界
            // 這裡我們直接使用您程式中已有的 FindVisualBoundingBox 方法，但需要做一點修改
            // 因為我們是黑白圖，所以判斷條件要改
            Rectangle visualBounds = FindVisualBoundingBox_For_BlackAndWhite(initialBmp);

            if (visualBounds.IsEmpty || visualBounds.Width <= 0 || visualBounds.Height <= 0)
            {
                initialBmp.Dispose();
                return new Bitmap(1, 1); // 如果是空白字元，返回一個小圖
            }

            // 步驟 3: 根據精確的視覺邊界，裁剪出最終的點陣圖
            Bitmap finalBmp = new Bitmap(visualBounds.Width, visualBounds.Height);
            using (Graphics g = Graphics.FromImage(finalBmp))
            {
                g.Clear(Color.White);
                // DrawImage 可以用來執行裁剪：
                // 將 initialBmp 中 visualBounds 區域的內容，畫到 finalBmp 的 (0,0) 位置
                g.DrawImage(initialBmp,
                            new Rectangle(0, 0, finalBmp.Width, finalBmp.Height), // 目標矩形
                            visualBounds, // 來源矩形
                            GraphicsUnit.Pixel);
            }

            initialBmp.Dispose(); // 釋放中介圖片的資源
            return finalBmp;
        }

        // 這是針對黑白點陣圖優化的 FindVisualBoundingBox
        private Rectangle FindVisualBoundingBox_For_BlackAndWhite(Bitmap bmp)
        {
            int top = -1, bottom = -1, left = -1, right = -1;

            // 從上到下
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (bmp.GetPixel(x, y).R == 0) // 黑色
                    {
                        top = y;
                        goto FoundTop;
                    }
                }
            }
        FoundTop:
            if (top == -1) return Rectangle.Empty; // 全白

            // 從下到上
            for (int y = bmp.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (bmp.GetPixel(x, y).R == 0)
                    {
                        bottom = y;
                        goto FoundBottom;
                    }
                }
            }
        FoundBottom:

            // 從左到右
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = top; y <= bottom; y++)
                {
                    if (bmp.GetPixel(x, y).R == 0)
                    {
                        left = x;
                        goto FoundLeft;
                    }
                }
            }
        FoundLeft:

            // 從右到左
            for (int x = bmp.Width - 1; x >= 0; x--)
            {
                for (int y = top; y <= bottom; y++)
                {
                    if (bmp.GetPixel(x, y).R == 0)
                    {
                        right = x;
                        goto FoundRight;
                    }
                }
            }
        FoundRight:

            return new Rectangle(left, top, right - left + 1, bottom - top + 1);
        }


        // Bitmap 轉矩陣，灰階判斷
        private int[,] BitmapToMatrix(Bitmap bmp)
        {
            int[,] matrix = new int[bmp.Height, bmp.Width];
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color p = bmp.GetPixel(x, y);
                    int gray = (int)(0.3 * p.R + 0.59 * p.G + 0.11 * p.B);
                    matrix[y, x] = gray < 128 ? 1 : 0; // 黑色=1
                }
            }
            return matrix;
        }

        private Bitmap DrawMatrixPreview(int[,] matrix, int scale = 20)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            Bitmap bmp = new Bitmap(cols * scale, rows * scale);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < cols; x++)
                    {
                        Rectangle rect = new Rectangle(x * scale, y * scale, scale, scale);
                        if (matrix[y, x] == 1)
                        {
                            g.FillRectangle(Brushes.Black, rect);
                        }
                        g.DrawRectangle(Pens.Gray, rect); // 畫格子線
                    }
                }
            }
            return bmp;
        }

        /// <summary>
        /// 將矩陣轉換為適用於 SH1106/SSD1306 (垂直掃描) 的 HEX 字串。
        /// </summary>
        private string MatrixToHexString_For_SH1106(int[,] matrix)
        {
            StringBuilder sb = new StringBuilder();
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // 計算總共需要多少頁 (每頁8行高)
            int numPages = (rows + 7) / 8;

            sb.AppendLine("{");
            sb.Append("  ");

            // 外層迴圈遍歷「頁」
            for (int p = 0; p < numPages; p++)
            {
                // 內層迴圈遍歷「欄」(水平方向)
                for (int x = 0; x < cols; x++)
                {
                    int value = 0;
                    // 這個迴圈為當前的 (頁, 欄) 組合建立一個 byte
                    // 我們需要從該頁的底部到頂部讀取，以便使用簡單的左移操作
                    // 或者從頂部到底部讀取，並使用 | 操作符
                    for (int bit = 0; bit < 8; bit++)
                    {
                        int y = (p * 8) + bit; // 計算實際的 y 座標

                        // 檢查是否超出陣列邊界 (對於高度不是8倍數的字體)
                        if (y < rows)
                        {
                            if (matrix[y, x] == 1)
                            {
                                // 最上面的像素 (bit=0) 是 LSB (D0)
                                // 所以我們將 1 左移 bit 位
                                value |= (1 << bit);
                            }
                        }
                    }
                    sb.Append($"0x{value:X2}, ");
                }
                sb.AppendLine($" // Page {p}"); // 為每頁資料加上註解，方便閱讀
            }

            sb.AppendLine("};");
            return sb.ToString();
        }


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
        private string MatrixToHexString(int[,] matrix)
        {
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
                    bitCount++;           // 每處理一個像素，計數器加一

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

                // --- 檢查是否已打包滿一個位元組 ---
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
        }
        /*
                /// <summary>
                /// 掃描 Bitmap，找出非白色像素構成的最小邊界矩形。
                /// </summary>
                /// <param name="bmp">要掃描的 Bitmap</param>
                /// <returns>包含實際內容的矩形區域</returns>
                private RectangleF FindVisualBoundingBox(Bitmap bmp)
                {
                    int top = -1, bottom = -1, left = -1, right = -1;

                    // 從上到下掃描，找第一個有內容的 y 座標 (top)
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            if (bmp.GetPixel(x, y).R < 250) // 容忍一點點反鋸齒的灰階
                            {
                                top = y;
                                goto FoundTop;
                            }
                        }
                    }
                FoundTop:
                    if (top == -1) return RectangleF.Empty; // 全白圖片

                    // 從下到上掃描，找第一個有內容的 y 座標 (bottom)
                    for (int y = bmp.Height - 1; y >= 0; y--)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            if (bmp.GetPixel(x, y).R < 250)
                            {
                                bottom = y;
                                goto FoundBottom;
                            }
                        }
                    }
                FoundBottom:

                    // 從左到右掃描，找第一個有內容的 x 座標 (left)
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        for (int y = top; y <= bottom; y++)
                        {
                            if (bmp.GetPixel(x, y).R < 250)
                            {
                                left = x;
                                goto FoundLeft;
                            }
                        }
                    }
                FoundLeft:

                    // 從右到左掃描，找第一個有內容的 x 座標 (right)
                    for (int x = bmp.Width - 1; x >= 0; x--)
                    {
                        for (int y = top; y <= bottom; y++)
                        {
                            if (bmp.GetPixel(x, y).R < 250)
                            {
                                right = x;
                                goto FoundRight;
                            }
                        }
                    }
                FoundRight:

                    // 加上一點 padding 避免筆劃被切到
                    int padding = 2;
                    left = Math.Max(0, left - padding);
                    right = Math.Min(bmp.Width - 1, right + padding);
                    top = Math.Max(0, top - padding);
                    bottom = Math.Min(bmp.Height - 1, bottom + padding);

                    return new RectangleF(left, top, right - left + 1, bottom - top + 1);
                }

                */

    }
    //public partial class FontToLCD : Form end



}
