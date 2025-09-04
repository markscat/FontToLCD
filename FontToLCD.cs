
using System;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
//using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;


namespace FontToLCD
    {
    public partial class FontToLCD : Form
    {

        /// <summary>
        /// 用於儲存「字型設計」分頁中，使用者正在編輯的點陣矩陣。
        /// </summary>
        private int[,]? _dotDesignMatrix;

        // (可選) 將繪圖的縮放比例也設為變數，方便統一修改
        private int _designPixelScale = 20;

        // 存最後一次生成的矩陣
        private int[,]? _lastGeneratedMatrix; // 新增：用於儲存 Button1_Click 生成的矩陣




        public FontToLCD()
        {
            InitializeComponent();
            InitForm();


            // 手動綁定按鈕事件（如果設計師文件中沒有）
            SafeFontArray.Click += SafeFontArray_Click;

            SafeCreateFont.Click += SafeCreateFont_Click;


            DesignCanvas.MouseClick += DesignCanvas_MouseClick;
            CreateGridButton.Click += CreateGridButton_Click;
            GenerateHexButton.Click += GenerateHexButton_Click;

        }

        private void InitForm()
        {
            // 初始化字型 ComboBox
            foreach (FontFamily ff in FontFamily.Families)
                FontType.Items.Add(ff.Name);
            FontType.SelectedIndex = 0;

            // 初始化大小 
            // 預設 8
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

            //
            Design_HEX_Type.Items.AddRange(new object[] {
                "通用水平掃描(單色)",
                "OLED SH1106/SSD1306(垂直)",
                "TFT ILI9225(彩色 RGB565)"
            });
            Design_HEX_Type.SelectedIndex = 1; // 預設選擇第一個選項

            

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
                _lastGeneratedMatrix = matrix;

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


        //
        /// <summary>
        /// 一個輔助函式，負責根據 _dotDesignMatrix 的當前狀態，更新畫布和 HEX 輸出。
        /// </summary>
        /// <summary>
        /// (修改後) 只負責根據 _dotDesignMatrix 的當前狀態，更新畫布。
        /// </summary>
        private void RedrawCanvas()
        {
            if (_dotDesignMatrix == null) return;
            DesignCanvas.Image = DrawMatrixPreview(_dotDesignMatrix, _designPixelScale);
        }


        /// <summary>
        /// 一個輔助函式，負責根據 _dotDesignMatrix 的當前狀態，更新畫布和 HEX 輸出。
        /// </summary>
        private void UpdateHexOutput()
        {

            if (_dotDesignMatrix == null)
            {
                DesignHexOutput.Text = "請先點擊「新建/清除畫布」來開始設計。";
                return;
            }

            // --- 2. 更新右邊的 HEX 輸出 (共用整個轉換器架構) ---
            // 這段邏輯和您第一個分頁的 button1_Click 中的轉換邏輯完全一樣！
            IScreenConverter? converter = null;

            string selectedScreen = Design_HEX_Type.SelectedItem?.ToString() ?? "";


            //Design_HEX_Type.Items.AddRange(new object[] {
            //    "通用水平掃描(單色)",
            //    "OLED SH1106/SSD1306(垂直)",
            //    "TFT ILI9225(彩色 RGB565)"
            //});

            switch (selectedScreen)
            {
                case "通用水平掃描(單色)":
                    converter = new HorizontalMonoConverter();
                    break;
                case "OLED SH1106/SSD1306(垂直)": // 修正名稱

                    converter = new Sh1106Converter();
                    break;
                case "TFT ILI9225(彩色 RGB565)":

                    converter = new Ili9225Converter();
                    break;
                default:
                    DesignHexOutput.Text = "請在第一個分頁選擇有效的 HEX 輸出類型。";
                    return;
            }

            // 對於點陣設計，前景是黑，背景是白
            Color foregroundColor = Color.Black;
            Color backgroundColor = Color.White;

            // 呼叫轉換器，得到 HEX 字串
            DesignHexOutput.Text = converter.Convert(_dotDesignMatrix, foregroundColor, backgroundColor);
        }


        private void CreateGridButton_Click(object? sender, EventArgs e)
        {
            // 1. 從 NumericUpDown 控制項讀取使用者想要的尺寸
            //    (請確保您在設計師中，已經將這兩個控制項命名為 GridWidthNumeric 和 GridHeightNumeric)
            int width = (int)GridWidthNumeric.Value;
            int height = (int)GridHeightNumeric.Value;

            // 2. 邊界檢查 (可選但推薦)，防止使用者輸入 0 或過大的值
            if (width <= 0 || height <= 0)
            {
                MessageBox.Show("畫布寬度和高度必須大於 0！");
                return;
            }

            //
            //
            //3. 根據讀取到的尺寸，建立一個全新的、全為 0 的二維陣列
            //    這會覆蓋掉舊的 _dotDesignMatrix
            _dotDesignMatrix = new int[height, width];

            // 4. 呼叫輔助函式來重繪空白畫布並更新 HEX
            RedrawCanvas();   // 這會畫出一個空白的網格
            UpdateHexOutput(); // 這會顯示一堆 0x00
        }


        private void DesignCanvas_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_dotDesignMatrix == null) return;

            // 1. 獲取當前矩陣的尺寸。
            //    _dotDesignMatrix.GetLength(0) 獲取第一個維度的大小 (高度/行數)。
            //    _dotDesignMatrix.GetLength(1) 獲取第二個維度的大小 (寬度/欄數)。
            int height = _dotDesignMatrix.GetLength(0);
            int width = _dotDesignMatrix.GetLength(1);

            // 2. 將滑鼠的點擊座標 (e.X, e.Y) 轉換為矩陣的索引 (gridX, gridY)。
            //    _designPixelScale 是您之前定義的、每個像素格子放大的倍率 (例如 20)。
            int gridX = e.X / _designPixelScale;
            int gridY = e.Y / _designPixelScale;

            if (gridX >= 0 && gridX < width && gridY >= 0 && gridY < height)

            {
                _dotDesignMatrix[gridY, gridX] = 1 - _dotDesignMatrix[gridY, gridX];
                RedrawCanvas(); // 只重繪畫布，不再更新 HEX
            }
        }

        private void GenerateHexButton_Click(object? sender, EventArgs e)
        {
            UpdateHexOutput(); // 只更新 HEX
        }

        private void SafeFontArray_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("SafeFontArray 按鈕被點擊了！", "測試", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (_lastGeneratedMatrix == null || _lastGeneratedMatrix.Length == 0 || string.IsNullOrWhiteSpace(Hex.Text))
            {
                MessageBox.Show("「字元轉換」分頁沒有可供儲存的數據，請先點擊「轉換」按鈕。", "儲存失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string character = Imput.Text.Length > 0 ? Imput.Text.Substring(0, 1) : "無字元";

            //string character = Imput.Text.Substring(0, 1);
            string fontName = FontType.SelectedItem?.ToString() ?? "Arial";
            float fontSize = float.TryParse(FontSize.SelectedItem?.ToString(), out float fs) ? fs : 16;
            string hexText = Hex.Text;


            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "字型數據文件 (*.h)|*.h|所有文件 (*.*)|*.*";
                sfd.Title = "儲存字元轉換數據";
                sfd.FileName = $"Char_{character}_Font_{fontName}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";

                SaveHexAndJson(_lastGeneratedMatrix, character, fontName, fontSize, hexText);

            }
        }

        private void SafeCreateFont_Click(object? sender, EventArgs e)
        {

            MessageBox.Show("SafeFontArray 按鈕被點擊了！", "測試", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (_dotDesignMatrix == null)
            {
                MessageBox.Show("請先在「字型設計」分頁創建網格", "儲存失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //int[,] matrix = BitmapToMatrixFromDesignCanvas(); // 你的 DesignCanvas 對應矩陣
            string character = "Custom";
            string fontName = "CustomDesign";
            float fontSize = 0;
            string hexText = DesignHexOutput.Text;

            SaveHexAndJson(_dotDesignMatrix, character, fontName, fontSize, hexText);
        }



        /// <summary>
        /// 實際執行儲存 .h 和 .json 檔案的邏輯。
        /// </summary>
        /// <param name="matrix">要儲存的點陣矩陣。</param>
        /// <param name="character">字元描述。</param>
        /// <param name="fontName">字型名稱。</param>
        /// <param name="fontSize">字型大小。</param>
        /// <param name="hexText">HEX 字串。</param>
        /// <param name="baseFilePath">不含副檔名的完整檔案路徑 (例如: C:\MyFonts\Font_20231027_103000)。</param>
        ///

        private void SaveHexAndJson(int[,] matrix, string character, string fontName, float fontSize, string hexText)
        {
            if (matrix == null || matrix.Length == 0 || string.IsNullOrWhiteSpace(hexText))
            {
                MessageBox.Show("資料不完整，無法存檔！");
                return;
            }

            try
            {
                // 確認 Font 資料夾存在
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Font");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // 用時間當檔名
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string baseFileName = $"Font_{timestamp}";

                // 儲存 .h 檔
                string hexFilePath = Path.Combine(folderPath, $"{baseFileName}.h");
                File.WriteAllText(hexFilePath, hexText, Encoding.UTF8);

                // 準備 JSON 數據
                var fontData = new
                {
                    Character = character,
                    FontName = fontName,
                    FontSize = fontSize,
                    MatrixWidth = matrix.GetLength(1),
                    MatrixHeight = matrix.GetLength(0),
                    MatrixData = ConvertMatrixToStringArray(matrix), // 將二維陣列轉為字串陣列
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                // 儲存 .json 檔
                string json = JsonSerializer.Serialize(fontData, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                string jsonFilePath = Path.Combine(folderPath, $"{baseFileName}.json");
                File.WriteAllText(jsonFilePath, json, Encoding.UTF8);

                MessageBox.Show($"已成功存檔！\n.h 檔: {hexFilePath}\n.json 檔: {jsonFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"存檔失敗: {ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 輔助方法：將二維陣列轉換為字串陣列（方便 JSON 儲存）
        private string[] ConvertMatrixToStringArray(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            string[] result = new string[rows];

            for (int y = 0; y < rows; y++)
            {
                StringBuilder rowBuilder = new StringBuilder();
                for (int x = 0; x < cols; x++)
                {
                    rowBuilder.Append(matrix[y, x].ToString());
                }
                result[y] = rowBuilder.ToString();
            }

            return result;
        }

        /*
        private void SaveHexAndJson(int[,] matrix, string character, string fontName, float fontSize, string hexText)
        {
            if (matrix == null || matrix.Length == 0 || string.IsNullOrWhiteSpace(hexText))
            {
                MessageBox.Show("資料不完整，無法存檔！");
                return;
            }

            // 確認資料夾
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "font");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // 日期時間檔名
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            //H file
            string hexFile = Path.Combine(folderPath, $"Font_{timestamp}.h");
            File.WriteAllText(hexFile, Hex.Text);


            // Json file
            var fontData = new
            {
                Character = character,
                FontName = fontName,
                FontSize = fontSize,
                Matrix = matrix // 直接儲存二維陣列
            };

            string json = JsonSerializer.Serialize(fontData, new JsonSerializerOptions { WriteIndented = true });
            string jsonFile = Path.Combine(folderPath, $"Font_{timestamp}.json");
            File.WriteAllText(jsonFile, json);

            MessageBox.Show($"已成功存檔到資料夾:\n{hexFile}\n{jsonFile}");
        }
        */
    }
    //public partial class FontToLCD : Form end



}
