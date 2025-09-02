
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
                // ��l�Ʀr�� ComboBox
                foreach (FontFamily ff in FontFamily.Families)
                FontType.Items.Add(ff.Name);
                FontType.SelectedIndex = 0;

                // ��l�Ƥj�p ComboBox
            //    LedFont.Items.AddRange(new object[] { "8", "16", "32" });
            //    LedFont.SelectedIndex = 0; // �w�] 8
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

        /// <summary>
        /// �N�x�}�ഫ���A�Ω� SH1106/SSD1306 (�������y) �� HEX �r��C
        /// </summary>
        private string MatrixToHexString_For_SH1106(int[,] matrix)
        {
            StringBuilder sb = new StringBuilder();
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // �p���`�@�ݭn�h�֭� (�C��8�氪)
            int numPages = (rows + 7) / 8;

            sb.AppendLine("{");
            sb.Append("  ");

            // �~�h�j��M���u���v
            for (int p = 0; p < numPages; p++)
            {
                // ���h�j��M���u��v(������V)
                for (int x = 0; x < cols; x++)
                {
                    int value = 0;
                    // �o�Ӱj�鬰��e�� (��, ��) �զX�إߤ@�� byte
                    // �ڭ̻ݭn�q�ӭ��������쳻��Ū���A�H�K�ϥ�²�檺�����ާ@
                    // �Ϊ̱q�����쩳��Ū���A�èϥ� | �ާ@��
                    for (int bit = 0; bit < 8; bit++)
                    {
                        int y = (p * 8) + bit; // �p���ڪ� y �y��

                        // �ˬd�O�_�W�X�}�C��� (��󰪫פ��O8���ƪ��r��)
                        if (y < rows)
                        {
                            if (matrix[y, x] == 1)
                            {
                                // �̤W�������� (bit=0) �O LSB (D0)
                                // �ҥH�ڭ̱N 1 ���� bit ��
                                value |= (1 << bit);
                            }
                        }
                    }
                    sb.Append($"0x{value:X2}, ");
                }
                sb.AppendLine($" // Page {p}"); // ���C����ƥ[�W���ѡA��K�\Ū
            }

            sb.AppendLine("};");
            return sb.ToString();
        }


        /// <summary>
        /// �N 0/1 ���G���x�}�ഫ�� C/C++ ���檺�Q���i��}�C�r��C
        /// �o�خ榡�A�Ω�u�������y�v�Ρu��D�� (Row-Major)�v����ܾ��C
        /// </summary>
        /// <param name="matrix">�]�t 0 �M 1 ���G����ư}�C�A�N�����I�}�C</param>
        /// <returns>�榡�ƫ᪺�Q���i��}�C�r��C</returns>
        /// <remarks>
        /// --- �榡�W�� ---
        /// 1. ���y��V�G�ѤW��U (Row by Row)�A�ѥ���k (Pixel by Pixel)�C
        /// 2. �������]�G�C 8 �ӹ������]���@�Ӧ줸�� (Byte)�C
        /// 3. �줸���� (Bit Order)�G�i���n�j�@�椤�A�̥��䪺�����O�̰����Ħ� (MSB-First)�C
        ///    �Ҧp�A�����ǦC [1,0,0,0,0,0,0,0] �|�Q�ഫ�� 0b10000000�A�]�N�O 0x80�C
        /// 4. �����B�z�G�p�G�@�湳�����e�פ��O 8 �����ơA�̫�@�Ӧ줸�ժ��Ѿl�줸�N�|�V������A�k��� 0�C
        ///    �Ҧp�A�@�榳 10 �ӹ����A�e 8 �ӥ��]���@�� Byte�A�� 2 �ӷ|���]�� `[P8, P9, 0, 0, 0, 0, 0, 0]`�C
        /// 
        /// --- �p��ק� ---
        /// - �Y�n�אּ�u�������y�v(�A�Ω� SH1106/SSD1306)�A�ݭn�N���~��h for �j�骺 y �M x ��աA�í��s�]�p�줸���]�޿�C
        /// - �Y�n�אּ�u�̧C���Ħ�b�e (LSB-First)�v�A�ݭn�ק�줸���]�������A
        /// �Ҧp�אּ `value = value | (matrix[y, x] << bitCount);` �æb���]�� 8 ��ɤ���줸���ǡA�Ϊ����q�k�V���֥[�C
        /// </remarks>
        /// 
        private string MatrixToHexString(int[,] matrix)
        {
            StringBuilder sb = new StringBuilder();
            int rows = matrix.GetLength(0);// ����x�}������ (�`�@���X��)
            int cols = matrix.GetLength(1);// ����x�}���e�� (�`�@���X��)

            sb.AppendLine("{");


            // --- �~�h�j��G�v��B�z ---
            // �o�Ӱj��q�Ĥ@�� (y=0) �}�l�A�@���B�z��̫�@��C
            for (int y = 0; y < rows; y++)
            {
                sb.Append("  ");    // �b�C�@�檺�}�Y�[�W�Y�ơA�Ϯ榡����[
                int value = 0;      // �Ω��x�s��e���b���]�� 8 �줸�줸�ժ���
                int bitCount = 0;   // �p�ƾ��A�p���e�줸�դw�g���]�F�X�Ӧ줸


                // --- ���h�j��G�B�z�@�椤���C�@�ӹ��� ---
                // �o�Ӱj��q�̥��䪺���� (x=0) �}�l�A�B�z��̥k��C
                for (int x = 0; x < cols; x++)
                {
                    // --- �֤ߦ줸���]�޿� (MSB-First) ---
                    // 1. `(Value << 1)`: �N�ثe���ȦV�����ʤ@��A���s���줸�˥X�Ŷ� (�̧C�� D0 �|�ܦ� 0)�C
                    // 2. `| matrix[y, x]`: �N�s�������� (0 �� 1) �z�L�u�Ρv�B��A��J���˥X���̧C��C
                    // �o�Ӿާ@���� 8 ����A�Ĥ@�Ӷi�Ӫ������N�|�Q����̰��� (D7)�C

                    value = (value << 1) | matrix[y, x];
                    bitCount++;           // �C�B�z�@�ӹ����A�p�ƾ��[�@

                    // --- �ˬd�O�_�w���]���@�Ӧ줸�� ---
                    if (bitCount == 8)
                    {
                        // �p�G�w�g���]�F 8 �Ӧ줸�A�N�N���ഫ���Q���i��r��ê��[
                        // "X2" �榡�ƫ��O��ܡGX=�j�g�Q���i��A2=�`�O��ܨ��� (�Ҧp 7 �|�ܦ� 07)
                        sb.Append($"0x{value:X2}, ");
                        // ���]�ܼơA�ǳƥ��]�U�@�Ӧ줸��
                        value = 0;
                        bitCount = 0;
                    }
                }

                // --- �ˬd�O�_�w���]���@�Ӧ줸�� ---
                if (bitCount > 0)
                {
                    // �N�Ѿl���줸�V�����ʡA����񺡤@�� 8 �쪺�줸�աC
                    // �o�˥i�H�T�O�Ѿl��������󰪦�A�ӧC��|�۰ʸ� 0�C
                    // �Ҧp�A�Y�Ѿl 3 �Ӧ줸 (bitCount=3)�A�h�ݭn���� 8-3=5 ���C
                    value <<= (8 - bitCount);
                    sb.Append($"0x{value:X2}, ");
                }
                // �B�z���@���A����H�W�[�iŪ��
                sb.AppendLine();
            }
            sb.AppendLine("};");
            return sb.ToString();
        }
        /*
                /// <summary>
                /// ���y Bitmap�A��X�D�զ⹳���c�����̤p��ɯx�ΡC
                /// </summary>
                /// <param name="bmp">�n���y�� Bitmap</param>
                /// <returns>�]�t��ڤ��e���x�ΰϰ�</returns>
                private RectangleF FindVisualBoundingBox(Bitmap bmp)
                {
                    int top = -1, bottom = -1, left = -1, right = -1;

                    // �q�W��U���y�A��Ĥ@�Ӧ����e�� y �y�� (top)
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            if (bmp.GetPixel(x, y).R < 250) // �e�Ԥ@�I�I�Ͽ������Ƕ�
                            {
                                top = y;
                                goto FoundTop;
                            }
                        }
                    }
                FoundTop:
                    if (top == -1) return RectangleF.Empty; // ���չϤ�

                    // �q�U��W���y�A��Ĥ@�Ӧ����e�� y �y�� (bottom)
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

                    // �q����k���y�A��Ĥ@�Ӧ����e�� x �y�� (left)
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

                    // �q�k�쥪���y�A��Ĥ@�Ӧ����e�� x �y�� (right)
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

                    // �[�W�@�I padding �קK�����Q����
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
