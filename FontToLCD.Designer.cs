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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FontToLCD));
            FontType = new ComboBox();
            Imput = new TextBox();
            label1 = new Label();
            label3 = new Label();
            BitView = new PictureBox();
            label4 = new Label();
            Hex = new TextBox();
            button1 = new Button();
            panel1 = new Panel();
            SafeFontArray = new Button();
            label2 = new Label();
            HEX_Type = new ComboBox();
            label5 = new Label();
            FontSize = new ComboBox();
            scrollPanelForBitView = new Panel();
            array_Tran = new TabControl();
            Font_Tran = new TabPage();
            DotFontDesign = new TabPage();
            panel2 = new Panel();
            SafeCreateFont = new Button();
            label8 = new Label();
            Design_HEX_Type = new ComboBox();
            GenerateHexButton = new Button();
            DesignHexOutput = new TextBox();
            scrollPanelForDesignCanvas = new Panel();
            DesignCanvas = new PictureBox();
            CreateGridButton = new Button();
            label7 = new Label();
            label6 = new Label();
            GridHeightNumeric = new NumericUpDown();
            GridWidthNumeric = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)BitView).BeginInit();
            panel1.SuspendLayout();
            scrollPanelForBitView.SuspendLayout();
            array_Tran.SuspendLayout();
            Font_Tran.SuspendLayout();
            DotFontDesign.SuspendLayout();
            panel2.SuspendLayout();
            scrollPanelForDesignCanvas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DesignCanvas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GridHeightNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)GridWidthNumeric).BeginInit();
            SuspendLayout();
            // 
            // FontType
            // 
            FontType.FormattingEnabled = true;
            FontType.Location = new Point(9, 39);
            FontType.Margin = new Padding(2);
            FontType.Name = "FontType";
            FontType.Size = new Size(163, 23);
            FontType.TabIndex = 0;
            // 
            // Imput
            // 
            Imput.Location = new Point(9, 92);
            Imput.Margin = new Padding(2);
            Imput.Name = "Imput";
            Imput.Size = new Size(356, 23);
            Imput.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(54, 21);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 3;
            label1.Text = "選擇字型";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(9, 74);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 5;
            label3.Text = "輸入字元";
            // 
            // BitView
            // 
            BitView.Location = new Point(9, 2);
            BitView.Margin = new Padding(2);
            BitView.Name = "BitView";
            BitView.Size = new Size(515, 386);
            BitView.SizeMode = PictureBoxSizeMode.AutoSize;
            BitView.TabIndex = 6;
            BitView.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(674, 98);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(31, 15);
            label4.TabIndex = 7;
            label4.Text = "預覽";
            // 
            // Hex
            // 
            Hex.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Hex.Location = new Point(544, 129);
            Hex.Margin = new Padding(2);
            Hex.Multiline = true;
            Hex.Name = "Hex";
            Hex.ReadOnly = true;
            Hex.ScrollBars = ScrollBars.Both;
            Hex.Size = new Size(348, 410);
            Hex.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(791, 94);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(73, 23);
            button1.TabIndex = 9;
            button1.Text = "生成矩陣";
            button1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(SafeFontArray);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(HEX_Type);
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
            panel1.Location = new Point(2, 5);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(903, 566);
            panel1.TabIndex = 10;
            // 
            // SafeFontArray
            // 
            SafeFontArray.Location = new Point(789, 38);
            SafeFontArray.Name = "SafeFontArray";
            SafeFontArray.Size = new Size(75, 23);
            SafeFontArray.TabIndex = 15;
            SafeFontArray.Text = "存檔";
            SafeFontArray.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(436, 15);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 14;
            label2.Text = "hex類型";
            // 
            // HEX_Type
            // 
            HEX_Type.FormattingEnabled = true;
            HEX_Type.Location = new Point(399, 39);
            HEX_Type.Margin = new Padding(2);
            HEX_Type.Name = "HEX_Type";
            HEX_Type.Size = new Size(139, 23);
            HEX_Type.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(212, 19);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(75, 15);
            label5.TabIndex = 11;
            label5.Text = "字型大小(pt)";
            // 
            // FontSize
            // 
            FontSize.FormattingEnabled = true;
            FontSize.Location = new Point(203, 39);
            FontSize.Margin = new Padding(2);
            FontSize.Name = "FontSize";
            FontSize.Size = new Size(118, 23);
            FontSize.TabIndex = 10;
            // 
            // scrollPanelForBitView
            // 
            scrollPanelForBitView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            scrollPanelForBitView.AutoScroll = true;
            scrollPanelForBitView.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scrollPanelForBitView.Controls.Add(BitView);
            scrollPanelForBitView.Location = new Point(0, 129);
            scrollPanelForBitView.Margin = new Padding(2);
            scrollPanelForBitView.Name = "scrollPanelForBitView";
            scrollPanelForBitView.Size = new Size(538, 409);
            scrollPanelForBitView.TabIndex = 12;
            // 
            // array_Tran
            // 
            array_Tran.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            array_Tran.Controls.Add(Font_Tran);
            array_Tran.Controls.Add(DotFontDesign);
            array_Tran.Location = new Point(5, 9);
            array_Tran.Margin = new Padding(2);
            array_Tran.Name = "array_Tran";
            array_Tran.SelectedIndex = 0;
            array_Tran.Size = new Size(915, 600);
            array_Tran.TabIndex = 11;
            // 
            // Font_Tran
            // 
            Font_Tran.Controls.Add(panel1);
            Font_Tran.Location = new Point(4, 24);
            Font_Tran.Margin = new Padding(2);
            Font_Tran.Name = "Font_Tran";
            Font_Tran.Padding = new Padding(2);
            Font_Tran.Size = new Size(907, 572);
            Font_Tran.TabIndex = 0;
            Font_Tran.Text = "字元轉換";
            Font_Tran.UseVisualStyleBackColor = true;
            // 
            // DotFontDesign
            // 
            DotFontDesign.Controls.Add(panel2);
            DotFontDesign.Location = new Point(4, 24);
            DotFontDesign.Margin = new Padding(2);
            DotFontDesign.Name = "DotFontDesign";
            DotFontDesign.Padding = new Padding(2);
            DotFontDesign.Size = new Size(907, 572);
            DotFontDesign.TabIndex = 1;
            DotFontDesign.Text = "字型設計";
            DotFontDesign.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(SafeCreateFont);
            panel2.Controls.Add(label8);
            panel2.Controls.Add(Design_HEX_Type);
            panel2.Controls.Add(GenerateHexButton);
            panel2.Controls.Add(DesignHexOutput);
            panel2.Controls.Add(scrollPanelForDesignCanvas);
            panel2.Controls.Add(CreateGridButton);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(GridHeightNumeric);
            panel2.Controls.Add(GridWidthNumeric);
            panel2.Location = new Point(0, 2);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(905, 546);
            panel2.TabIndex = 0;
            // 
            // SafeCreateFont
            // 
            SafeCreateFont.Location = new Point(693, 10);
            SafeCreateFont.Name = "SafeCreateFont";
            SafeCreateFont.Size = new Size(75, 23);
            SafeCreateFont.TabIndex = 18;
            SafeCreateFont.Text = "存檔";
            SafeCreateFont.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(535, 14);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(53, 15);
            label8.TabIndex = 17;
            label8.Text = "Hex格式";
            // 
            // Design_HEX_Type
            // 
            Design_HEX_Type.FormattingEnabled = true;
            Design_HEX_Type.Location = new Point(499, 32);
            Design_HEX_Type.Margin = new Padding(2);
            Design_HEX_Type.Name = "Design_HEX_Type";
            Design_HEX_Type.Size = new Size(118, 23);
            Design_HEX_Type.TabIndex = 16;
            // 
            // GenerateHexButton
            // 
            GenerateHexButton.Location = new Point(677, 42);
            GenerateHexButton.Margin = new Padding(2);
            GenerateHexButton.Name = "GenerateHexButton";
            GenerateHexButton.Size = new Size(111, 23);
            GenerateHexButton.TabIndex = 15;
            GenerateHexButton.Text = "產生/更新 HEX";
            GenerateHexButton.UseVisualStyleBackColor = true;
            // 
            // DesignHexOutput
            // 
            DesignHexOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DesignHexOutput.Location = new Point(570, 65);
            DesignHexOutput.Margin = new Padding(2);
            DesignHexOutput.Multiline = true;
            DesignHexOutput.Name = "DesignHexOutput";
            DesignHexOutput.ReadOnly = true;
            DesignHexOutput.ScrollBars = ScrollBars.Both;
            DesignHexOutput.Size = new Size(323, 460);
            DesignHexOutput.TabIndex = 14;
            // 
            // scrollPanelForDesignCanvas
            // 
            scrollPanelForDesignCanvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            scrollPanelForDesignCanvas.AutoScroll = true;
            scrollPanelForDesignCanvas.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            scrollPanelForDesignCanvas.Controls.Add(DesignCanvas);
            scrollPanelForDesignCanvas.Location = new Point(5, 65);
            scrollPanelForDesignCanvas.Margin = new Padding(2);
            scrollPanelForDesignCanvas.Name = "scrollPanelForDesignCanvas";
            scrollPanelForDesignCanvas.Size = new Size(561, 477);
            scrollPanelForDesignCanvas.TabIndex = 13;
            // 
            // DesignCanvas
            // 
            DesignCanvas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            DesignCanvas.Location = new Point(2, 10);
            DesignCanvas.Margin = new Padding(2);
            DesignCanvas.Name = "DesignCanvas";
            DesignCanvas.Size = new Size(686, 602);
            DesignCanvas.SizeMode = PictureBoxSizeMode.AutoSize;
            DesignCanvas.TabIndex = 6;
            DesignCanvas.TabStop = false;
            // 
            // CreateGridButton
            // 
            CreateGridButton.Location = new Point(291, 36);
            CreateGridButton.Margin = new Padding(2);
            CreateGridButton.Name = "CreateGridButton";
            CreateGridButton.Size = new Size(118, 23);
            CreateGridButton.TabIndex = 4;
            CreateGridButton.Text = "新建/清除畫布";
            CreateGridButton.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(174, 21);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(31, 15);
            label7.TabIndex = 3;
            label7.Text = "高度";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(45, 21);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 2;
            label6.Text = "寬度";
            // 
            // GridHeightNumeric
            // 
            GridHeightNumeric.Location = new Point(136, 39);
            GridHeightNumeric.Margin = new Padding(2);
            GridHeightNumeric.Name = "GridHeightNumeric";
            GridHeightNumeric.Size = new Size(117, 23);
            GridHeightNumeric.TabIndex = 1;
            // 
            // GridWidthNumeric
            // 
            GridWidthNumeric.Location = new Point(5, 39);
            GridWidthNumeric.Margin = new Padding(2);
            GridWidthNumeric.Name = "GridWidthNumeric";
            GridWidthNumeric.Size = new Size(117, 23);
            GridWidthNumeric.TabIndex = 0;
            // 
            // FontToLCD
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(923, 611);
            Controls.Add(array_Tran);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "FontToLCD";
            Text = "FontToLCD";
            ((System.ComponentModel.ISupportInitialize)BitView).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            scrollPanelForBitView.ResumeLayout(false);
            scrollPanelForBitView.PerformLayout();
            array_Tran.ResumeLayout(false);
            Font_Tran.ResumeLayout(false);
            DotFontDesign.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            scrollPanelForDesignCanvas.ResumeLayout(false);
            scrollPanelForDesignCanvas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DesignCanvas).EndInit();
            ((System.ComponentModel.ISupportInitialize)GridHeightNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)GridWidthNumeric).EndInit();
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
        private Label label2;
        private ComboBox HEX_Type;
        private TabControl array_Tran;
        private TabPage Font_Tran;
        private TabPage DotFontDesign;
        private Panel panel2;
        private Label label6;
        private NumericUpDown GridHeightNumeric;
        private NumericUpDown GridWidthNumeric;
        private Panel scrollPanelForDesignCanvas;
        private PictureBox DesignCanvas;
        private Button CreateGridButton;
        private Label label7;
        private TextBox DesignHexOutput;
        private Button GenerateHexButton;
        private Label label8;
        private ComboBox Design_HEX_Type;
        private Button SafeFontArray;
        private Button SafeCreateFont;
    }
}
