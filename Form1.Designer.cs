namespace FontToLCD
{
    partial class FontToLCD
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            FontType = new ComboBox();
            Imput = new TextBox();
            LedFont = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            BitView = new PictureBox();
            label4 = new Label();
            Hex = new TextBox();
            button1 = new Button();
            panel1 = new Panel();
            label5 = new Label();
            FontSize = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)BitView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // FontType
            // 
            FontType.FormattingEnabled = true;
            FontType.Location = new Point(12, 49);
            FontType.Name = "FontType";
            FontType.Size = new Size(208, 27);
            FontType.TabIndex = 0;
            // 
            // Imput
            // 
            Imput.Location = new Point(12, 116);
            Imput.Name = "Imput";
            Imput.Size = new Size(457, 27);
            Imput.TabIndex = 1;
            // 
            // LedFont
            // 
            LedFont.FormattingEnabled = true;
            LedFont.Location = new Point(700, 47);
            LedFont.Name = "LedFont";
            LedFont.Size = new Size(151, 27);
            LedFont.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(69, 27);
            label1.Name = "label1";
            label1.Size = new Size(69, 19);
            label1.TabIndex = 3;
            label1.Text = "選擇字型";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(709, 25);
            label2.Name = "label2";
            label2.Size = new Size(129, 19);
            label2.TabIndex = 4;
            label2.Text = "選擇輸出字型大小";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 94);
            label3.Name = "label3";
            label3.Size = new Size(69, 19);
            label3.TabIndex = 5;
            label3.Text = "輸入字元";
            // 
            // BitView
            // 
            BitView.Location = new Point(12, 163);
            BitView.Name = "BitView";
            BitView.Size = new Size(494, 483);
            BitView.TabIndex = 6;
            BitView.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(152, 153);
            label4.Name = "label4";
            label4.Size = new Size(39, 19);
            label4.TabIndex = 7;
            label4.Text = "預覽";
            // 
            // Hex
            // 
            Hex.Location = new Point(546, 174);
            Hex.Multiline = true;
            Hex.Name = "Hex";
            Hex.ReadOnly = true;
            Hex.ScrollBars = ScrollBars.Both;
            Hex.Size = new Size(457, 472);
            Hex.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(909, 47);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 9;
            button1.Text = "生成矩陣";
            button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(label5);
            panel1.Controls.Add(FontSize);
            panel1.Controls.Add(Hex);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(BitView);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(FontType);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(Imput);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(LedFont);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1024, 679);
            panel1.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(272, 24);
            label5.Name = "label5";
            label5.Size = new Size(93, 19);
            label5.TabIndex = 11;
            label5.Text = "字型大小(pt)";
            // 
            // FontSize
            // 
            FontSize.FormattingEnabled = true;
            FontSize.Location = new Point(261, 53);
            FontSize.Name = "FontSize";
            FontSize.Size = new Size(151, 27);
            FontSize.TabIndex = 10;
            // 
            // FontToLCD
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1039, 703);
            Controls.Add(panel1);
            Controls.Add(label4);
            Name = "FontToLCD";
            Text = "FontToLCD";
            ((System.ComponentModel.ISupportInitialize)BitView).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox FontType;
        private TextBox Imput;
        private ComboBox LedFont;
        private Label label1;
        private Label label2;
        private Label label3;
        private PictureBox BitView;
        private Label label4;
        private TextBox Hex;
        private Button button1;
        private Panel panel1;
        private Label label5;
        private ComboBox FontSize;
    }
}
