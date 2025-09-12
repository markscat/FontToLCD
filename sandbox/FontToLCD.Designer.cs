namespace FontToLCD
{
    partial class FontToLCD
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
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
            MergeFontH = new TabPage();
            SafeMergeFile = new Button();
            panel5 = new Panel();
            MergeFile3 = new TextBox();
            MergeFile = new Button();
            panel4 = new Panel();
            File2 = new TextBox();
            labelFile2 = new Label();
            ReadFile2 = new Button();
            panel3 = new Panel();
            File1 = new TextBox();
            ReadFile1 = new Button();
            labelFile1 = new Label();
            Fontlib = new TabPage();
            Format = new Label();
            label15 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            Array_Dimensions = new Label();
            GlyphSize = new Label();
            Bounding_Box = new Label();
            Character = new Label();
            SaveDotFont = new Button();
            splitContainer1 = new SplitContainer();
            CharacterListBox = new ListBox();
            DotFontReview = new PictureBox();
            FullFontArray = new TextBox();
            DotFontGen = new Button();
            label10 = new Label();
            SelectFontSize = new ComboBox();
            label9 = new Label();
            SelectFontType = new ComboBox();
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
            MergeFontH.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            Fontlib.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DotFontReview).BeginInit();
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
            BitView.Size = new Size(466, 405);
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
            Hex.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Hex.Location = new Point(488, 129);
            Hex.Margin = new Padding(2);
            Hex.Multiline = true;
            Hex.Name = "Hex";
            Hex.ReadOnly = true;
            Hex.ScrollBars = ScrollBars.Both;
            Hex.Size = new Size(404, 410);
            Hex.TabIndex = 8;
            // 
            // button1
            // 
            button1.Location = new Point(488, 94);
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
            scrollPanelForBitView.Size = new Size(487, 409);
            scrollPanelForBitView.TabIndex = 12;
            // 
            // array_Tran
            // 
            array_Tran.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            array_Tran.Controls.Add(Font_Tran);
            array_Tran.Controls.Add(DotFontDesign);
            array_Tran.Controls.Add(MergeFontH);
            array_Tran.Controls.Add(Fontlib);
            array_Tran.Location = new Point(5, 9);
            array_Tran.Margin = new Padding(2);
            array_Tran.Name = "array_Tran";
            array_Tran.SelectedIndex = 0;
            array_Tran.Size = new Size(933, 600);
            array_Tran.TabIndex = 11;
            // 
            // Font_Tran
            // 
            Font_Tran.Controls.Add(panel1);
            Font_Tran.Location = new Point(4, 24);
            Font_Tran.Margin = new Padding(2);
            Font_Tran.Name = "Font_Tran";
            Font_Tran.Padding = new Padding(2);
            Font_Tran.Size = new Size(925, 572);
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
            DotFontDesign.Size = new Size(925, 572);
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
            panel2.Size = new Size(891, 546);
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
            DesignHexOutput.Size = new Size(309, 460);
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
            // MergeFontH
            // 
            MergeFontH.Controls.Add(SafeMergeFile);
            MergeFontH.Controls.Add(panel5);
            MergeFontH.Controls.Add(panel4);
            MergeFontH.Controls.Add(panel3);
            MergeFontH.Location = new Point(4, 24);
            MergeFontH.Name = "MergeFontH";
            MergeFontH.Padding = new Padding(3);
            MergeFontH.Size = new Size(925, 572);
            MergeFontH.TabIndex = 2;
            MergeFontH.Text = "合併字型檔";
            MergeFontH.UseVisualStyleBackColor = true;
            // 
            // SafeMergeFile
            // 
            SafeMergeFile.Location = new Point(9, 10);
            SafeMergeFile.Name = "SafeMergeFile";
            SafeMergeFile.Size = new Size(75, 23);
            SafeMergeFile.TabIndex = 9;
            SafeMergeFile.Text = "存檔";
            SafeMergeFile.UseVisualStyleBackColor = true;
            SafeMergeFile.Click += SafeMergeFile_Click;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel5.Controls.Add(MergeFile3);
            panel5.Controls.Add(MergeFile);
            panel5.Location = new Point(618, 39);
            panel5.Name = "panel5";
            panel5.Size = new Size(300, 530);
            panel5.TabIndex = 8;
            // 
            // MergeFile3
            // 
            MergeFile3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MergeFile3.Location = new Point(3, 57);
            MergeFile3.Multiline = true;
            MergeFile3.Name = "MergeFile3";
            MergeFile3.ReadOnly = true;
            MergeFile3.ScrollBars = ScrollBars.Both;
            MergeFile3.Size = new Size(294, 470);
            MergeFile3.TabIndex = 1;
            // 
            // MergeFile
            // 
            MergeFile.Location = new Point(20, 11);
            MergeFile.Name = "MergeFile";
            MergeFile.Size = new Size(75, 23);
            MergeFile.TabIndex = 0;
            MergeFile.Text = "合併檔案";
            MergeFile.UseVisualStyleBackColor = true;
            MergeFile.Click += MergeFile_Click;
            // 
            // panel4
            // 
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel4.Controls.Add(File2);
            panel4.Controls.Add(labelFile2);
            panel4.Controls.Add(ReadFile2);
            panel4.Location = new Point(312, 39);
            panel4.Name = "panel4";
            panel4.Size = new Size(300, 530);
            panel4.TabIndex = 7;
            // 
            // File2
            // 
            File2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            File2.Location = new Point(3, 57);
            File2.Multiline = true;
            File2.Name = "File2";
            File2.ReadOnly = true;
            File2.ScrollBars = ScrollBars.Both;
            File2.Size = new Size(294, 470);
            File2.TabIndex = 5;
            // 
            // labelFile2
            // 
            labelFile2.AutoSize = true;
            labelFile2.Location = new Point(3, 39);
            labelFile2.Name = "labelFile2";
            labelFile2.Size = new Size(38, 15);
            labelFile2.TabIndex = 3;
            labelFile2.Text = "檔案2";
            // 
            // ReadFile2
            // 
            ReadFile2.Location = new Point(3, 11);
            ReadFile2.Name = "ReadFile2";
            ReadFile2.Size = new Size(75, 23);
            ReadFile2.TabIndex = 1;
            ReadFile2.Text = "讀取檔案2";
            ReadFile2.UseVisualStyleBackColor = true;
            ReadFile2.Click += ReadFile2_Click;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel3.AutoScroll = true;
            panel3.Controls.Add(File1);
            panel3.Controls.Add(ReadFile1);
            panel3.Controls.Add(labelFile1);
            panel3.Location = new Point(6, 39);
            panel3.Name = "panel3";
            panel3.Size = new Size(300, 530);
            panel3.TabIndex = 6;
            // 
            // File1
            // 
            File1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            File1.Location = new Point(3, 57);
            File1.Multiline = true;
            File1.Name = "File1";
            File1.ReadOnly = true;
            File1.ScrollBars = ScrollBars.Both;
            File1.Size = new Size(294, 470);
            File1.TabIndex = 4;
            // 
            // ReadFile1
            // 
            ReadFile1.Location = new Point(3, 3);
            ReadFile1.Name = "ReadFile1";
            ReadFile1.Size = new Size(75, 23);
            ReadFile1.TabIndex = 0;
            ReadFile1.Text = "讀取檔案1";
            ReadFile1.UseVisualStyleBackColor = true;
            ReadFile1.Click += ReadFile1_Click;
            // 
            // labelFile1
            // 
            labelFile1.AutoSize = true;
            labelFile1.Location = new Point(3, 39);
            labelFile1.Name = "labelFile1";
            labelFile1.Size = new Size(38, 15);
            labelFile1.TabIndex = 2;
            labelFile1.Text = "檔案1";
            // 
            // Fontlib
            // 
            Fontlib.Controls.Add(Format);
            Fontlib.Controls.Add(label15);
            Fontlib.Controls.Add(label11);
            Fontlib.Controls.Add(label12);
            Fontlib.Controls.Add(label13);
            Fontlib.Controls.Add(label14);
            Fontlib.Controls.Add(Array_Dimensions);
            Fontlib.Controls.Add(GlyphSize);
            Fontlib.Controls.Add(Bounding_Box);
            Fontlib.Controls.Add(Character);
            Fontlib.Controls.Add(SaveDotFont);
            Fontlib.Controls.Add(splitContainer1);
            Fontlib.Controls.Add(DotFontGen);
            Fontlib.Controls.Add(label10);
            Fontlib.Controls.Add(SelectFontSize);
            Fontlib.Controls.Add(label9);
            Fontlib.Controls.Add(SelectFontType);
            Fontlib.Location = new Point(4, 24);
            Fontlib.Name = "Fontlib";
            Fontlib.Padding = new Padding(3);
            Fontlib.Size = new Size(925, 572);
            Fontlib.TabIndex = 3;
            Fontlib.Text = "點矩陣自行轉換";
            Fontlib.UseVisualStyleBackColor = true;
            // 
            // Format
            // 
            Format.AutoSize = true;
            Format.Location = new Point(626, 65);
            Format.Name = "Format";
            Format.Size = new Size(55, 15);
            Format.TabIndex = 16;
            Format.Text = "資料格式";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(565, 65);
            label15.Name = "label15";
            label15.Size = new Size(55, 15);
            label15.TabIndex = 15;
            label15.Text = "資料格式";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(565, 50);
            label11.Name = "label11";
            label11.Size = new Size(55, 15);
            label11.TabIndex = 14;
            label11.Text = "陣列維度";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(565, 35);
            label12.Name = "label12";
            label12.Size = new Size(55, 15);
            label12.TabIndex = 13;
            label12.Text = "實際尺寸";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(565, 20);
            label13.Name = "label13";
            label13.Size = new Size(55, 15);
            label13.TabIndex = 12;
            label13.Text = "標準尺寸";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(565, 3);
            label14.Name = "label14";
            label14.Size = new Size(55, 15);
            label14.TabIndex = 11;
            label14.Text = "目前字元";
            // 
            // Array_Dimensions
            // 
            Array_Dimensions.AutoSize = true;
            Array_Dimensions.Location = new Point(626, 50);
            Array_Dimensions.Name = "Array_Dimensions";
            Array_Dimensions.Size = new Size(55, 15);
            Array_Dimensions.TabIndex = 10;
            Array_Dimensions.Text = "陣列維度";
            // 
            // GlyphSize
            // 
            GlyphSize.AutoSize = true;
            GlyphSize.Location = new Point(626, 35);
            GlyphSize.Name = "GlyphSize";
            GlyphSize.Size = new Size(55, 15);
            GlyphSize.TabIndex = 9;
            GlyphSize.Text = "實際尺寸";
            // 
            // Bounding_Box
            // 
            Bounding_Box.AutoSize = true;
            Bounding_Box.Location = new Point(626, 20);
            Bounding_Box.Name = "Bounding_Box";
            Bounding_Box.Size = new Size(55, 15);
            Bounding_Box.TabIndex = 8;
            Bounding_Box.Text = "標準尺寸";
            // 
            // Character
            // 
            Character.AutoSize = true;
            Character.Location = new Point(626, 3);
            Character.Name = "Character";
            Character.Size = new Size(55, 15);
            Character.TabIndex = 7;
            Character.Text = "目前字元";
            // 
            // SaveDotFont
            // 
            SaveDotFont.Location = new Point(814, 48);
            SaveDotFont.Name = "SaveDotFont";
            SaveDotFont.Size = new Size(75, 23);
            SaveDotFont.TabIndex = 6;
            SaveDotFont.Text = "存檔";
            SaveDotFont.UseVisualStyleBackColor = true;
            SaveDotFont.Click += SaveDotFont_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(0, 91);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(CharacterListBox);
            splitContainer1.Panel1.Controls.Add(DotFontReview);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(FullFontArray);
            splitContainer1.Size = new Size(922, 475);
            splitContainer1.SplitterDistance = 455;
            splitContainer1.TabIndex = 5;
            // 
            // CharacterListBox
            // 
            CharacterListBox.FormattingEnabled = true;
            CharacterListBox.ItemHeight = 15;
            CharacterListBox.Location = new Point(6, 0);
            CharacterListBox.Name = "CharacterListBox";
            CharacterListBox.Size = new Size(446, 109);
            CharacterListBox.TabIndex = 17;
            // 
            // DotFontReview
            // 
            DotFontReview.Location = new Point(3, 115);
            DotFontReview.Name = "DotFontReview";
            DotFontReview.Size = new Size(449, 360);
            DotFontReview.TabIndex = 0;
            DotFontReview.TabStop = false;
            DotFontReview.Paint += DotFontReview_Paint;
            // 
            // FullFontArray
            // 
            FullFontArray.Location = new Point(3, 3);
            FullFontArray.Multiline = true;
            FullFontArray.Name = "FullFontArray";
            FullFontArray.ReadOnly = true;
            FullFontArray.ScrollBars = ScrollBars.Both;
            FullFontArray.Size = new Size(457, 502);
            FullFontArray.TabIndex = 0;
            FullFontArray.TabStop = false;
            // 
            // DotFontGen
            // 
            DotFontGen.Location = new Point(353, 48);
            DotFontGen.Name = "DotFontGen";
            DotFontGen.Size = new Size(75, 23);
            DotFontGen.TabIndex = 4;
            DotFontGen.Text = "產生點矩陣字形";
            DotFontGen.UseVisualStyleBackColor = true;
            DotFontGen.Click += DotFontGen_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(243, 22);
            label10.Name = "label10";
            label10.Size = new Size(75, 15);
            label10.TabIndex = 3;
            label10.Text = "字型大小(pt)";
            // 
            // SelectFontSize
            // 
            SelectFontSize.FormattingEnabled = true;
            SelectFontSize.Location = new Point(216, 44);
            SelectFontSize.Name = "SelectFontSize";
            SelectFontSize.Size = new Size(121, 23);
            SelectFontSize.TabIndex = 2;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(70, 22);
            label9.Name = "label9";
            label9.Size = new Size(55, 15);
            label9.TabIndex = 1;
            label9.Text = "選擇字形";
            // 
            // SelectFontType
            // 
            SelectFontType.FormattingEnabled = true;
            SelectFontType.Location = new Point(6, 44);
            SelectFontType.Name = "SelectFontType";
            SelectFontType.Size = new Size(183, 23);
            SelectFontType.TabIndex = 0;
            // 
            // FontToLCD
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(941, 611);
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
            MergeFontH.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            Fontlib.ResumeLayout(false);
            Fontlib.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DotFontReview).EndInit();
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
        private TabPage MergeFontH;
        private Button ReadFile2;
        private Button ReadFile1;
        private Label labelFile2;
        private Label labelFile1;
        private Panel panel5;
        private Panel panel4;
        private TextBox File2;
        private Panel panel3;
        private TextBox File1;
        private TextBox MergeFile3;
        private Button MergeFile;
        private Button SafeMergeFile;

        private TabPage Fontlib;
        private Label label9;
        private ComboBox SelectFontType;
        private ComboBox SelectFontSize;
        private Label label10;
        private Button DotFontGen;
        private SplitContainer splitContainer1;
        private PictureBox DotFontReview;
        private TextBox FullFontArray;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label Array_Dimensions;
        private Label GlyphSize;
        private Label Bounding_Box;
        private Label Character;
        private Button SaveDotFont;
        private Label Format;
        private Label label15;
        private ListBox CharacterListBox;
    }
}
