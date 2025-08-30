
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
            }

        private void Button1_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Imput.Text)) return;

            string text = Imput.Text;
            string fontName = FontType.SelectedItem?.ToString() ?? "Arial";
            int fontSize = int.TryParse(FontSize.SelectedItem?.ToString(), out int fs) ? fs : 16;

            // �o�̥��ο�X���j�p�M�w�x�}�j�p (��: 8 �� 8x16, 16 �� 16x16, 32 �� 32x32)
            int bmpWidth = fontSize;

            int bmpHeight = (fontSize <= 8) ? fontSize : fontSize * 2; // �p�r�� 1:1�A�j�r�� 1:2 ���

            // �e�r
            using (Bitmap bmp = DrawCharToBitmap(text, fontName, fontSize, bmpWidth, bmpHeight))          
            
            {
                // ��x�}
                int[,] matrix = BitmapToMatrix(bmp);

                // �w���]��j��ܡ^
                BitView.Image = DrawMatrixPreview(matrix, 20);

                // �� HEX
                Hex.Text = MatrixToHexString(matrix);
            }
        }



        private Bitmap DrawCharToBitmap(string text, string fontName, float fontSize, int targetWidth, int targetHeight)
        {
            int scale = 8; // ��j���ơA�i�վ�
            int bigWidth = targetWidth * scale;
            int bigHeight = targetHeight * scale;


            // 1. �j�ؤo�e�r
            Bitmap bigBmp = new Bitmap(bigWidth, bigHeight);

            using (Graphics g = Graphics.FromImage(bigBmp))
            {
                g.Clear(Color.White);             
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                using (Font font = new Font(fontName, bigHeight * 0.8f, FontStyle.Regular, GraphicsUnit.Pixel))

                //using (Font font = new Font(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel))
                {

                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    g.DrawString(text, font, Brushes.Black, new RectangleF(0, 0, bigWidth, bigHeight), sf);
                }
            }

            // 2. �Y�p��ؼЯx�}�j�p
            Bitmap smallBmp = new Bitmap(targetWidth, targetHeight);
            using (Graphics g = Graphics.FromImage(smallBmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bigBmp, new Rectangle(0, 0, targetWidth, targetHeight),
                            new Rectangle(0, 0, bigWidth, bigHeight),
                            GraphicsUnit.Pixel);
            }

            bigBmp.Dispose();
            return smallBmp;
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
        
        private string MatrixToHexString(int[,] matrix)
        {
            StringBuilder sb = new StringBuilder();
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            sb.AppendLine("{");
            for (int y = 0; y < rows; y++)
            {
                sb.Append("  ");
                int value = 0;
                int bitCount = 0;

                for (int x = 0; x < cols; x++)
                {
                    value = (value << 1) | matrix[y, x];
                    bitCount++;

                    if (bitCount == 8)
                    {
                        sb.Append($"0x{value:X2}, ");
                        value = 0;
                        bitCount = 0;
                    }
                }

                // �p�G�e�פ��O8�����ơA�ɻ��̫�@�� byte
                if (bitCount > 0)
                {
                    value <<= (8 - bitCount);
                    sb.Append($"0x{value:X2}, ");
                }

                sb.AppendLine();
            }
            sb.AppendLine("};");
            return sb.ToString();
        }


        /*
            private void Button1_Click(object? sender, EventArgs e){
            if (string.IsNullOrEmpty(Imput.Text)) return;
            if (FontType.SelectedItem == null || FontSize.SelectedItem == null) return;
            
            string text = Imput.Text;
            string fontName = FontType.SelectedItem.ToString() ?? "Arial";
            int selectedPt = int.Parse(FontSize.SelectedItem.ToString() ?? "8");
            int fontSize = int.TryParse(FontSize.SelectedItem?.ToString(), out int fs) ? fs : 16;


            // �۰ʨM�w�x�}�ؤo
            int matrixWidth = fontSize;
            int matrixHeight= fontSize * 2;

            using (Bitmap bmp = new Bitmap(matrixWidth, matrixHeight))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

                // ��r�~�����
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;


            }




                if (selectedPt <= 6)        { matrixWidth = 6; matrixHeight = 12; }
                else if (selectedPt <= 8)   { matrixWidth = 8; matrixHeight = 16; }
                else if (selectedPt <= 12)  { matrixWidth = 12; matrixHeight = 16; }
                else                        { matrixWidth = 16; matrixHeight = 16; }


            // �p��r���j�p
                fontSize = matrixHeight * 0.8f; // �d�I��Z
                Bitmap bmp = DrawCharToBitmap(text, fontName, fontSize, matrixWidth, matrixHeight);


                // ��x�}
                int[,] matrix = BitmapToMatrix(bmp);

                // ��ܹw��
                BitView.Image = DrawMatrixPreview(matrix, 20);

                // ��� HEX
                Hex.Text = MatrixToHexString(matrix);
        }
        */


        /*
            private Bitmap DrawCharToBitmap(string text, string fontName, int size)
            {
                Bitmap bmp = new Bitmap(size, size);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    using (Font font = new Font(fontName, size))
                    {
                        g.DrawString(text, font, Brushes.Black, -1, -2); // �L�զ�m
                    }
                }
                return bmp;
            }*/

        /*
            private string MatrixToString(int[,] matrix)
            {
                StringBuilder sb = new StringBuilder();
                int rows = matrix.GetLength(0);
                int cols = matrix.GetLength(1);
                sb.AppendLine("{");
                for (int y = 0; y < rows; y++)
                {
                    sb.Append("  { ");
                    for (int x = 0; x < cols; x++)
                    {
                        sb.Append(matrix[y, x]);
                        if (x != cols - 1) sb.Append(", ");
                    }
                    sb.AppendLine(" },");
                }
                sb.AppendLine("};");
                return sb.ToString();
            }
        */


        /*
        private Bitmap DrawCharToBitmap(string text, string fontName, int size)
        {
            // 1. ���Τj�ؤo�e����r����e�X��
            int bigSize = 32; // ��j�e��
            Bitmap bigBmp = new Bitmap(bigSize, bigSize);

            using (Graphics g = Graphics.FromImage(bigBmp))
            {
                g.Clear(Color.White);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;

                using (Font font = new Font(fontName, bigSize * 3 / 4, GraphicsUnit.Pixel))
                {
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    g.DrawString(text, font, Brushes.Black, new Rectangle(0, 0, bigSize, bigSize), format);
                }
            }

            // 2. �Y�p��ؼФؤo�]size �� size�^
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bigBmp, new Rectangle(0, 0, size, size));
            }

            return bmp;
        }
        */



        /*
        private Bitmap DrawCharToBitmap(string text, string fontName, int size)
        {
            // ���Τ���j���e���ӵe�r�A�קK�Q����
            int bigSize = size * 4; // ��j 4 ���e�A�A�Y�p
            Bitmap bigBmp = new Bitmap(bigSize, bigSize);

            using (Graphics g = Graphics.FromImage(bigBmp))
            {
                g.Clear(Color.White);

                using (Font font = new Font(fontName, bigSize * 3 / 4, GraphicsUnit.Pixel))
                {
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    Rectangle rect = new Rectangle(0, 0, bigSize, bigSize);
                    g.DrawString(text, font, Brushes.Black, rect, format);
                }
            }

            // �A�Y�p�� size �� size
            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(bigBmp, new Rectangle(0, 0, size, size));
            }

            return bmp;
        }

        */
    }
}
