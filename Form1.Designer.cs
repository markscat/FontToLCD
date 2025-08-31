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
            label1 = new Label();
            label3 = new Label();
            BitView = new PictureBox();
            label4 = new Label();
            Hex = new TextBox();
            button1 = new Button();
            panel1 = new Panel();
            label5 = new Label();
            FontSize = new ComboBox();
            scrollPanelForBitView = new Panel();
            ((System.ComponentModel.ISupportInitialize)BitView).BeginInit();
            panel1.SuspendLayout();
            scrollPanelForBitView.SuspendLayout();
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(69, 27);
            label1.Name = "label1";
            label1.Size = new Size(69, 19);
            label1.TabIndex = 3;
            label1.Text = "選擇字型";
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
            BitView.Location = new Point(0, 0);
            BitView.Name = "BitView";
            BitView.Size = new Size(665, 552);
            BitView.SizeMode = PictureBoxSizeMode.AutoSize;
            BitView.TabIndex = 6;
            BitView.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(937, 124);
            label4.Name = "label4";
            label4.Size = new Size(39, 19);
            label4.TabIndex = 7;
            label4.Text = "預覽";
            // 
            // Hex
            // 
            Hex.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Hex.Location = new Point(700, 163);
            Hex.Multiline = true;
            Hex.Name = "Hex";
            Hex.ReadOnly = true;
            Hex.ScrollBars = ScrollBars.Both;
            Hex.Size = new Size(446, 552);
            Hex.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(543, 116);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 9;
            button1.Text = "生成矩陣";
            button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(FontSize);
            panel1.Controls.Add(FontType);
            panel1.Controls.Add(Imput);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(Hex);
            panel1.Controls.Add(scrollPanelForBitView);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1170, 750);
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
            // scrollPanelForBitView
            // 
            scrollPanelForBitView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            scrollPanelForBitView.AutoScroll = true;
            scrollPanelForBitView.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scrollPanelForBitView.Controls.Add(BitView);
            scrollPanelForBitView.Location = new Point(0, 163);
            scrollPanelForBitView.Name = "scrollPanelForBitView";
            scrollPanelForBitView.Size = new Size(692, 552);
            scrollPanelForBitView.TabIndex = 12;
            // 
            // FontToLCD
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1187, 774);
            Controls.Add(panel1);
            Name = "FontToLCD";
            Text = "FontToLCD";
            ((System.ComponentModel.ISupportInitialize)BitView).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            scrollPanelForBitView.ResumeLayout(false);
            scrollPanelForBitView.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox FontType;
        private TextBox Imput;
        private Label label1;
        private Label label3;
        private PictureBox BitView;
        private Label label4;
        private TextBox Hex;
        private Button button1;
        private Panel panel1;
        private Label label5;
        private ComboBox FontSize;
        private Panel scrollPanelForBitView;
    }
}
