
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
        /// �Ω��x�s�u�r���]�p�v�������A�ϥΪ̥��b�s�誺�I�}�x�}�C
        /// </summary>
        private int[,]? _dotDesignMatrix;

        // (�i��) �Nø�Ϫ��Y���Ҥ]�]���ܼơA��K�Τ@�ק�
        private int _designPixelScale = 20;

        // �s�̫�@���ͦ����x�}
        private int[,]? _lastGeneratedMatrix; // �s�W�G�Ω��x�s Button1_Click �ͦ����x�}




        public FontToLCD()
        {
            InitializeComponent();
            InitForm();


            // ��ʸj�w���s�ƥ�]�p�G�]�p�v��󤤨S���^
            SafeFontArray.Click += SafeFontArray_Click;

            SafeCreateFont.Click += SafeCreateFont_Click;


            DesignCanvas.MouseClick += DesignCanvas_MouseClick;
            CreateGridButton.Click += CreateGridButton_Click;
            GenerateHexButton.Click += GenerateHexButton_Click;

        }

        private void InitForm()
        {
            // ��l�Ʀr�� ComboBox
            foreach (FontFamily ff in FontFamily.Families)
                FontType.Items.Add(ff.Name);
            FontType.SelectedIndex = 0;

            // ��l�Ƥj�p 
            // �w�] 8
            FontSize.Items.AddRange(new object[] { "8", "12", "16", "24", "32" });
            FontSize.SelectedIndex = 0;
            // ���s�ƥ�
            button1.Click += Button1_Click;

            // �w�]��r
            Imput.Text = "a";
            // ��l�ƥؼпù����� ComboBox
            HEX_Type.Items.AddRange(new object[] {
                "�q�Τ������y (���)",
                "OLED SH1106/SSD1306 (����)",
                "TFT ILI9225 (�m�� RGB565)"
            });
            HEX_Type.SelectedIndex = 0; // �w�]��ܲĤ@�ӿﶵ

            //
            Design_HEX_Type.Items.AddRange(new object[] {
                "�q�Τ������y(���)",
                "OLED SH1106/SSD1306(����)",
                "TFT ILI9225(�m�� RGB565)"
            });
            Design_HEX_Type.SelectedIndex = 1; // �w�]��ܲĤ@�ӿﶵ

            

        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Imput.Text)) return;

            string text = Imput.Text;
            string fontName = FontType.SelectedItem?.ToString() ?? "Arial";
            //int fontSize = int.TryParse(FontSize.SelectedItem?.ToString(), out int fs) ? fs : 16;
            float fontSize = float.TryParse(FontSize.SelectedItem?.ToString(), out float fs) ? fs : 16;

            int[,] matrix; // �Ψ��x�s�̲ת� 0/1 �x�}


            // �e�r
            using (Bitmap bmp = DrawCharToBitmap(text.Substring(0, 1), fontName, fontSize))
            {
                // ��x�}
                matrix = BitmapToMatrix(bmp);
                _lastGeneratedMatrix = matrix;

                // --- �B�J 2: �ھ� ComboBox ����ܡA�إ߹������ഫ�� ---
                // �o�N�O�u�u�t�Ҧ��v��²���{�A�ھڭq��]�ϥΪ̿�ܡ^�Ͳ��������u��C
                IScreenConverter? converter = null; // �ŧi�@�Ӥ����ܼơA���i�H��������@���ഫ��
                string selectedScreen = HEX_Type.SelectedItem?.ToString() ?? "";


                switch (selectedScreen)
                {
                    case "�q�Τ������y (���)":
                        converter = new HorizontalMonoConverter();
                        break;
                    case "OLED SH1106/SSD1306 (����)":
                        converter = new Sh1106Converter();
                        break;
                    case "TFT ILI9225 (�m�� RGB565)":
                        converter = new Ili9225Converter();
                        break;
                    default:
                        // �p�G�ϥΪ̨S����ܡA���X���ܨðh�X
                        MessageBox.Show("�п�ܤ@�Ӧ��Ī� HEX ��X�����I");
                        return;
                }


                // --- �B�J 3: �����ഫ�ç�s UI ---
                // ����C��C�Y�ϬO����ഫ���A�ڭ̤]�ǤJ�C��A�u�O�����|�ϥΦӤw�C
                // �o�̧ڭ̥ζ¦�M�զ�@���w�]�ȡC
                // �p�G�z�[�J�F�C���ܾ��A�N�q��������C��C
                Color foregroundColor = Color.Black;
                Color backgroundColor = Color.White;

                // �o�N�O��Ӭ[�c���u�����a��I
                // �L�� converter �쩳�O�����ഫ���A�ڭ̳��Χ����ۦP���覡�I�s���C
                // �D�{�����ݭn���D�ഫ������Ӹ`�C
                Hex.Text = converter.Convert(matrix, foregroundColor, backgroundColor);

                // �w���]��j��ܡ^
                BitView.Image = DrawMatrixPreview(matrix, 20);
            }
        }

        private Bitmap DrawCharToBitmap(string text, string fontName, float fontSize)
        {
            // �B�J 1: ���� MeasureString ���ͤ@�ӡu���j�v���B�]�t�ťժ���l�I�}��
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

                    // �إߪ�l�I�}��
                    initialBmp = new Bitmap(bmpWidth, bmpHeight);
                    using (Graphics initialG = Graphics.FromImage(initialBmp))
                    {
                        initialG.Clear(Color.White);
                        initialG.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                        initialG.DrawString(text, font, Brushes.Black, 0, 0);
                    }
                }
            }

            // �B�J 2: ���y�o�Ӫ�l�I�}�ϡA��X�r���������u�u���v��ı���
            // �o�̧ڭ̪����ϥαz�{�����w���� FindVisualBoundingBox ��k�A���ݭn���@�I�ק�
            // �]���ڭ̬O�¥չϡA�ҥH�P�_����n��
            Rectangle visualBounds = FindVisualBoundingBox_For_BlackAndWhite(initialBmp);

            if (visualBounds.IsEmpty || visualBounds.Width <= 0 || visualBounds.Height <= 0)
            {
                initialBmp.Dispose();
                return new Bitmap(1, 1); // �p�G�O�ťզr���A��^�@�Ӥp��
            }

            // �B�J 3: �ھں�T����ı��ɡA���ťX�̲ת��I�}��
            Bitmap finalBmp = new Bitmap(visualBounds.Width, visualBounds.Height);
            using (Graphics g = Graphics.FromImage(finalBmp))
            {
                g.Clear(Color.White);
                // DrawImage �i�H�ΨӰ�����šG
                // �N initialBmp �� visualBounds �ϰ쪺���e�A�e�� finalBmp �� (0,0) ��m
                g.DrawImage(initialBmp,
                            new Rectangle(0, 0, finalBmp.Width, finalBmp.Height), // �ؼЯx��
                            visualBounds, // �ӷ��x��
                            GraphicsUnit.Pixel);
            }

            initialBmp.Dispose(); // ���񤤤��Ϥ����귽
            return finalBmp;
        }

        // �o�O�w��¥��I�}���u�ƪ� FindVisualBoundingBox
        private Rectangle FindVisualBoundingBox_For_BlackAndWhite(Bitmap bmp)
        {
            int top = -1, bottom = -1, left = -1, right = -1;

            // �q�W��U
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (bmp.GetPixel(x, y).R == 0) // �¦�
                    {
                        top = y;
                        goto FoundTop;
                    }
                }
            }
        FoundTop:
            if (top == -1) return Rectangle.Empty; // ����

            // �q�U��W
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

            // �q����k
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

            // �q�k�쥪
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


        // Bitmap ��x�}�A�Ƕ��P�_
        private int[,] BitmapToMatrix(Bitmap bmp)
        {
            int[,] matrix = new int[bmp.Height, bmp.Width];
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color p = bmp.GetPixel(x, y);
                    int gray = (int)(0.3 * p.R + 0.59 * p.G + 0.11 * p.B);
                    matrix[y, x] = gray < 128 ? 1 : 0; // �¦�=1
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
                        g.DrawRectangle(Pens.Gray, rect); // �e��l�u
                    }
                }
            }
            return bmp;
        }


        //
        /// <summary>
        /// �@�ӻ��U�禡�A�t�d�ھ� _dotDesignMatrix ����e���A�A��s�e���M HEX ��X�C
        /// </summary>
        /// <summary>
        /// (�ק��) �u�t�d�ھ� _dotDesignMatrix ����e���A�A��s�e���C
        /// </summary>
        private void RedrawCanvas()
        {
            if (_dotDesignMatrix == null) return;
            DesignCanvas.Image = DrawMatrixPreview(_dotDesignMatrix, _designPixelScale);
        }


        /// <summary>
        /// �@�ӻ��U�禡�A�t�d�ھ� _dotDesignMatrix ����e���A�A��s�e���M HEX ��X�C
        /// </summary>
        private void UpdateHexOutput()
        {

            if (_dotDesignMatrix == null)
            {
                DesignHexOutput.Text = "�Х��I���u�s��/�M���e���v�Ӷ}�l�]�p�C";
                return;
            }

            // --- 2. ��s�k�䪺 HEX ��X (�@�ξ���ഫ���[�c) ---
            // �o�q�޿�M�z�Ĥ@�Ӥ����� button1_Click �����ഫ�޿觹���@�ˡI
            IScreenConverter? converter = null;

            string selectedScreen = Design_HEX_Type.SelectedItem?.ToString() ?? "";


            //Design_HEX_Type.Items.AddRange(new object[] {
            //    "�q�Τ������y(���)",
            //    "OLED SH1106/SSD1306(����)",
            //    "TFT ILI9225(�m�� RGB565)"
            //});

            switch (selectedScreen)
            {
                case "�q�Τ������y(���)":
                    converter = new HorizontalMonoConverter();
                    break;
                case "OLED SH1106/SSD1306(����)": // �ץ��W��

                    converter = new Sh1106Converter();
                    break;
                case "TFT ILI9225(�m�� RGB565)":

                    converter = new Ili9225Converter();
                    break;
                default:
                    DesignHexOutput.Text = "�Цb�Ĥ@�Ӥ�����ܦ��Ī� HEX ��X�����C";
                    return;
            }

            // ����I�}�]�p�A�e���O�¡A�I���O��
            Color foregroundColor = Color.Black;
            Color backgroundColor = Color.White;

            // �I�s�ഫ���A�o�� HEX �r��
            DesignHexOutput.Text = converter.Convert(_dotDesignMatrix, foregroundColor, backgroundColor);
        }


        private void CreateGridButton_Click(object? sender, EventArgs e)
        {
            // 1. �q NumericUpDown ���Ū���ϥΪ̷Q�n���ؤo
            //    (�нT�O�z�b�]�p�v���A�w�g�N�o��ӱ���R�W�� GridWidthNumeric �M GridHeightNumeric)
            int width = (int)GridWidthNumeric.Value;
            int height = (int)GridHeightNumeric.Value;

            // 2. ����ˬd (�i�������)�A����ϥΪ̿�J 0 �ιL�j����
            if (width <= 0 || height <= 0)
            {
                MessageBox.Show("�e���e�שM���ץ����j�� 0�I");
                return;
            }

            //
            //
            //3. �ھ�Ū���쪺�ؤo�A�إߤ@�ӥ��s���B���� 0 ���G���}�C
            //    �o�|�л\���ª� _dotDesignMatrix
            _dotDesignMatrix = new int[height, width];

            // 4. �I�s���U�禡�ӭ�ø�ťյe���ç�s HEX
            RedrawCanvas();   // �o�|�e�X�@�Ӫťժ�����
            UpdateHexOutput(); // �o�|��ܤ@�� 0x00
        }


        private void DesignCanvas_MouseClick(object? sender, MouseEventArgs e)
        {
            if (_dotDesignMatrix == null) return;

            // 1. �����e�x�}���ؤo�C
            //    _dotDesignMatrix.GetLength(0) ����Ĥ@�Ӻ��ת��j�p (����/���)�C
            //    _dotDesignMatrix.GetLength(1) ����ĤG�Ӻ��ת��j�p (�e��/���)�C
            int height = _dotDesignMatrix.GetLength(0);
            int width = _dotDesignMatrix.GetLength(1);

            // 2. �N�ƹ����I���y�� (e.X, e.Y) �ഫ���x�}������ (gridX, gridY)�C
            //    _designPixelScale �O�z���e�w�q���B�C�ӹ�����l��j�����v (�Ҧp 20)�C
            int gridX = e.X / _designPixelScale;
            int gridY = e.Y / _designPixelScale;

            if (gridX >= 0 && gridX < width && gridY >= 0 && gridY < height)

            {
                _dotDesignMatrix[gridY, gridX] = 1 - _dotDesignMatrix[gridY, gridX];
                RedrawCanvas(); // �u��ø�e���A���A��s HEX
            }
        }

        private void GenerateHexButton_Click(object? sender, EventArgs e)
        {
            UpdateHexOutput(); // �u��s HEX
        }

        private void SafeFontArray_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("SafeFontArray ���s�Q�I���F�I", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (_lastGeneratedMatrix == null || _lastGeneratedMatrix.Length == 0 || string.IsNullOrWhiteSpace(Hex.Text))
            {
                MessageBox.Show("�u�r���ഫ�v�����S���i���x�s���ƾڡA�Х��I���u�ഫ�v���s�C", "�x�s����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string character = Imput.Text.Length > 0 ? Imput.Text.Substring(0, 1) : "�L�r��";

            //string character = Imput.Text.Substring(0, 1);
            string fontName = FontType.SelectedItem?.ToString() ?? "Arial";
            float fontSize = float.TryParse(FontSize.SelectedItem?.ToString(), out float fs) ? fs : 16;
            string hexText = Hex.Text;


            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "�r���ƾڤ�� (*.h)|*.h|�Ҧ���� (*.*)|*.*";
                sfd.Title = "�x�s�r���ഫ�ƾ�";
                sfd.FileName = $"Char_{character}_Font_{fontName}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";

                SaveHexAndJson(_lastGeneratedMatrix, character, fontName, fontSize, hexText);

            }
        }

        private void SafeCreateFont_Click(object? sender, EventArgs e)
        {

            MessageBox.Show("SafeFontArray ���s�Q�I���F�I", "����", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (_dotDesignMatrix == null)
            {
                MessageBox.Show("�Х��b�u�r���]�p�v�����Ыغ���", "�x�s����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //int[,] matrix = BitmapToMatrixFromDesignCanvas(); // �A�� DesignCanvas �����x�}
            string character = "Custom";
            string fontName = "CustomDesign";
            float fontSize = 0;
            string hexText = DesignHexOutput.Text;

            SaveHexAndJson(_dotDesignMatrix, character, fontName, fontSize, hexText);
        }



        /// <summary>
        /// ��ڰ����x�s .h �M .json �ɮת��޿�C
        /// </summary>
        /// <param name="matrix">�n�x�s���I�}�x�}�C</param>
        /// <param name="character">�r���y�z�C</param>
        /// <param name="fontName">�r���W�١C</param>
        /// <param name="fontSize">�r���j�p�C</param>
        /// <param name="hexText">HEX �r��C</param>
        /// <param name="baseFilePath">���t���ɦW�������ɮ׸��| (�Ҧp: C:\MyFonts\Font_20231027_103000)�C</param>
        ///

        private void SaveHexAndJson(int[,] matrix, string character, string fontName, float fontSize, string hexText)
        {
            if (matrix == null || matrix.Length == 0 || string.IsNullOrWhiteSpace(hexText))
            {
                MessageBox.Show("��Ƥ�����A�L�k�s�ɡI");
                return;
            }

            try
            {
                // �T�{ Font ��Ƨ��s�b
                string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Font");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                // �ήɶ����ɦW
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string baseFileName = $"Font_{timestamp}";

                // �x�s .h ��
                string hexFilePath = Path.Combine(folderPath, $"{baseFileName}.h");
                File.WriteAllText(hexFilePath, hexText, Encoding.UTF8);

                // �ǳ� JSON �ƾ�
                var fontData = new
                {
                    Character = character,
                    FontName = fontName,
                    FontSize = fontSize,
                    MatrixWidth = matrix.GetLength(1),
                    MatrixHeight = matrix.GetLength(0),
                    MatrixData = ConvertMatrixToStringArray(matrix), // �N�G���}�C�ର�r��}�C
                    CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                // �x�s .json ��
                string json = JsonSerializer.Serialize(fontData, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

                string jsonFilePath = Path.Combine(folderPath, $"{baseFileName}.json");
                File.WriteAllText(jsonFilePath, json, Encoding.UTF8);

                MessageBox.Show($"�w���\�s�ɡI\n.h ��: {hexFilePath}\n.json ��: {jsonFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�s�ɥ���: {ex.Message}", "���~", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ���U��k�G�N�G���}�C�ഫ���r��}�C�]��K JSON �x�s�^
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
                MessageBox.Show("��Ƥ�����A�L�k�s�ɡI");
                return;
            }

            // �T�{��Ƨ�
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "font");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // ����ɶ��ɦW
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
                Matrix = matrix // �����x�s�G���}�C
            };

            string json = JsonSerializer.Serialize(fontData, new JsonSerializerOptions { WriteIndented = true });
            string jsonFile = Path.Combine(folderPath, $"Font_{timestamp}.json");
            File.WriteAllText(jsonFile, json);

            MessageBox.Show($"�w���\�s�ɨ��Ƨ�:\n{hexFile}\n{jsonFile}");
        }
        */
    }
    //public partial class FontToLCD : Form end



}
