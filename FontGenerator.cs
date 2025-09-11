using System;
using System.Collections.Generic;
using System.Drawing; // 需要 System.Drawing 來處理字型和點陣圖
using System.Text;
using System.Text.RegularExpressions;

namespace FontToLCD // 確保命名空間與您的主程式相同
{
    // 一個用來傳遞生成選項的類別，讓參數更清晰
    public class FontGenerationOptions
    {
        public string FontName { get; set; } = "Arial";
        public int FontSize { get; set; } = 8;
        public string CharactersToGenerate { get; set; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        // 未來可以增加更多選項，例如輸出格式 (水平/垂直)
    }

    // 這就是我們的核心邏輯類別
    public class FontGenerator
    {


        // 存合併後的字元資料
        private Dictionary<string, string> mergedFontData = new Dictionary<string, string>();

        // 目前的 converter / font / size
        private string _currentConverterType = "";
        private string _currentFontName = "";
        private string _currentFontSize = "";
        //private List<string> _currentCharacters = new List<string>();

        // 新增：儲存所有解析後的字元點陣資料
        // Dictionary 的 Key 是字元，Value 是 CharBitmapData 物件

        // Dictionary<TKey, TValue> 的主要功用就是：
        // 👉 用「Key」來快速找到對應的「Value」。
        // 有點像是一本字典（Key 是單字，Value 是解釋），所以叫 Dictionary。

        private Dictionary<string, CharBitmapData> _allCharBitmaps = new Dictionary<string, CharBitmapData>();


        // 新增：儲存所有檔案中找到的最大寬高 (位元組數和行數)
        private int _maxWidthBytes = 0; // 最大每行位元組數
        private int _maxHeight = 0;     // 最大行數

        // 類別成員變數，用於儲存第一個檔案的屬性
        private string _currentArrayDimensions = ""; // 新增: 用於儲存陣列維度，例如 "[8][16]"
        private List<string> _mergedHexData = new List<string>(); // 儲存只包含十六進位資料的部分

        // 選中的檔案路徑
        private string[] selectedFiles = Array.Empty<string>();





        public string MergeFontFiles(string[] selectedFiles)
        {
            if (selectedFiles == null || selectedFiles.Length == 0)
            {
                throw new Exception("請先選擇要合併的檔案！"); // 把 MessageBox.Show 改成 throw new Exception

                //return;
            }

            _allCharBitmaps.Clear(); // 清除之前的資料
            mergedFontData.Clear();
            _currentConverterType = "";
            _currentFontName = "";
            _currentFontSize = "";
            _maxWidthBytes = 0;
            _maxHeight = 0;
            //_currentArrayDimensions = ""; // 清除維度

            foreach (string filePath in selectedFiles)
            {
                string content = File.ReadAllText(filePath, Encoding.UTF8);
                string character = ExtractMeta(content, "Character").Trim();
                string converterType = ExtractMeta(content, "Converter Type").Trim();
                string fontName = ExtractMeta(content, "Font").Trim();
                string fontSize = ExtractMeta(content, "FontSize").Trim();
                //string arrayDimensions = ExtractArrayDimensions(content).Trim(); // 提取陣列維度

                // 第一個檔案設定全域 converter/font/size
                if (string.IsNullOrEmpty(_currentConverterType))
                {
                    _currentConverterType = converterType;
                    _currentFontName = fontName;
                    _currentFontSize = fontSize;
                    //_currentArrayDimensions = arrayDimensions; // 從第一個檔案設定維度
                }
                else
                {
                    if (converterType != _currentConverterType || fontName != _currentFontName || fontSize != _currentFontSize)
                    {
                        // 遇到錯誤時
                        throw new Exception($"檔案 {Path.GetFileName(filePath)} 無法解析..."); // 也改成 throw
                        //return;
                        //MessageBox.Show($"檔案 {Path.GetFileName(filePath)} 屬性不一致，無法合併！");
                        //return;
                    }
                }

                CharBitmapData? charBitmap = ParseCharBitmapData(content);
                if (charBitmap == null)
                {
                    // 將錯誤訊息拋出，讓 UI 層去決定如何顯示
                    throw new Exception($"合併中止！\n原因：無法解析點陣資料或維度。\n問題檔案：{Path.GetFileName(filePath)}");
                }

                // 更新最大維度
                _maxWidthBytes = Math.Max(_maxWidthBytes, charBitmap.Width);
                _maxHeight = Math.Max(_maxHeight, charBitmap.Height);

                foreach (string ch in character.Split(','))
                {
                    string c = ch.Trim();
                    if (_allCharBitmaps.ContainsKey(c))
                    {
                        // 如果字元重複，您可以選擇覆蓋、跳過或報錯
                        MessageBox.Show($"警告：字元 '{c}' 在多個檔案中重複出現，將保留最後一個檔案的定義。");
                        _allCharBitmaps[c] = charBitmap; // 覆蓋
                    }
                    else
                    {
                        _allCharBitmaps.Add(c, charBitmap);
                    }
                }
            }
            // 輸出合併檔案
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Font");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string hexFilePath = Path.Combine(folderPath, $"MergedFont_{timestamp}.h");


            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"// Converter Type: {_currentConverterType}");
            sb.AppendLine($"// Font: {_currentFontName}");
            sb.AppendLine($"// FontSize: {_currentFontSize}");
            sb.AppendLine($"// Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n");
            sb.AppendLine($"// Merged Width (bytes): {_maxWidthBytes}"); // 記錄合併後的維度
            sb.AppendLine($"// Merged Height (rows): {_maxHeight}");
            sb.AppendLine();


            // 建立一個新的陣列宣告，其維度是我們計算出的最大維度
            // 陣列名稱通常是 fontData 或類似的
            sb.AppendLine($"const uint8_t fontData[][{_maxWidthBytes}] = {{"); // 第一個維度動態，第二個是最大位元組寬度

            bool firstChar = true;
            // 按字元排序，讓輸出更整潔（可選）
            var sortedCharBitmaps = _allCharBitmaps.OrderBy(kvp => kvp.Key).ToList();

            foreach (var entry in sortedCharBitmaps)
            {
                string character = entry.Key;
                CharBitmapData originalBitmap = entry.Value;

                if (!firstChar)
                {
                    sb.AppendLine(","); // 在每個字元資料塊之間添加逗號
                    sb.AppendLine();    // 增加可讀性
                }

                sb.AppendLine($"  // Character: {character}");

                // 現在我們需要將原始 bitmap 調整到 _maxWidthBytes x _maxHeight，並「靠下對齊」

                // 計算需要填充的行數 (在上面)
                int paddingRows = _maxHeight - originalBitmap.Height;

                // 添加上方的填充行 (都是 0x00)
                for (int i = 0; i < paddingRows; i++)
                {
                    sb.Append("  {");
                    sb.Append(string.Join(", ", Enumerable.Repeat("0x00", _maxWidthBytes)));
                    sb.AppendLine("},");
                }

                // 添加原始資料行，並進行每行的寬度填充
                for (int i = 0; i < originalBitmap.Rows.Count; i++)
                {
                    byte[] originalRow = originalBitmap.Rows[i];
                    sb.Append("  {");
                    List<string> hexValuesInRow = new List<string>();

                    // 添加原始位元組
                    foreach (byte b in originalRow)
                    {
                        hexValuesInRow.Add($"0x{b:X2}");
                    }

                    // 在行末添加寬度填充 (0x00)
                    while (hexValuesInRow.Count < _maxWidthBytes)
                    {
                        hexValuesInRow.Add("0x00");
                    }

                    sb.Append(string.Join(", ", hexValuesInRow));

                    // 如果不是最後一個字元的最後一行，則添加逗號

                    if (!(entry.Key == sortedCharBitmaps.Last().Key && i == originalBitmap.Rows.Count - 1))
                    {
                        sb.AppendLine("},");
                    }
                    else
                    {
                        sb.AppendLine("}"); // 最後一個字元的最後一行不加逗號
                    }
                }
                firstChar = false;
            }

            sb.AppendLine("\n};"); // 關閉單一陣列宣告

            //File.WriteAllText(hexFilePath, sb.ToString(), Encoding.UTF8);
            //MergeFile3.Text = sb.ToString();
            //MessageBox.Show($"已成功合併，存檔到：\n{hexFilePath}");
            return sb.ToString(); // 這是方法的最終回傳值

        }




        // 這是對外提供的主要方法
        public string GenerateFontLibrary(FontGenerationOptions options)
        {
            StringBuilder sb = new StringBuilder();

            // 1. 批次處理的核心邏輯：定義基準
            //    (這裡可以計算所有字元，找出最大寬高，或者直接使用固定值)
            int maxWidth = 11; // 假設我們根據 8pt 字型算出了最大寬度
            int maxHeight = 8;  // 固定高度

            sb.AppendLine($"// Font Library: {options.FontName}, Size: {options.FontSize}pt");
            sb.AppendLine($"// Generated on: {DateTime.Now}");
            sb.AppendLine();

            // 2. 遍歷所有要生成的字元
            foreach (char c in options.CharactersToGenerate)
            {
                // 在這裡放置所有複雜的生成邏輯：
                // a. 使用 System.Drawing.Font 和 Graphics.DrawString 將字元畫到 Bitmap 上
                // b. 從 Bitmap 讀取像素資料
                // c. 將像素資料放入一個 maxWidth x maxHeight 的標準框中 (完成對齊)
                // d. 將對齊後的點陣資料轉換成 C 語言陣列字串

                string charArrayString = GenerateSingleCharArray(c, options, maxWidth, maxHeight);
                sb.AppendLine(charArrayString);
                sb.AppendLine();
            }

            return sb.ToString();
        }


        // 這是一個新的內部類別，用於儲存單一字元的點陣資料及其原始尺寸
        private class CharBitmapData
        {
            public int Width { get; set; } // 每個位元組代表的寬度 (例如 8 bits = 8 pixels)
            public int Height { get; set; } // 行數
            public List<byte[]> Rows { get; set; } // 每個 byte[] 儲存一行資料

            public CharBitmapData(int width, int height)
            {
                Width = width;
                Height = height;
                Rows = new List<byte[]>(height);
            }

            // 將二維 byte 陣列轉換為十六進位字串（用於最終輸出）
            public string ToHexString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{");
                bool firstRow = true;
                foreach (var row in Rows)
                {
                    if (!firstRow)
                    {
                        sb.Append(",\n"); // 每行後換行並加逗號
                    }
                    sb.Append("  "); // 縮排
                    bool firstByteInRow = true;
                    foreach (byte b in row)
                    {
                        if (!firstByteInRow)
                        {
                            sb.Append(", ");
                        }
                        sb.Append($"0x{b:X2}");
                        firstByteInRow = false;
                    }
                    firstRow = false;
                }
                sb.Append("\n}");
                return sb.ToString();
            }
        }

        private (int bytesPerRow, int height) GetArrayNumericDimensions(string content)
        {
            // =========================================================================
            // 第一遍掃描：使用 using 自動關閉 StringReader
            // =========================================================================
            using (StringReader sr = new StringReader(content))
            {
                string? line;
                int? charWidthFromComment = null;
                int? charHeightFromComment = null;

                while ((line = sr.ReadLine()) != null)
                {
                    // ================================================================
                    // 新增的邏輯 (最高優先級)：尋找 "5x5" 這種 WxH 格式的註解或名稱
                    // ================================================================
                    var matchWxH = Regex.Match(line, @"(\d+)x(\d+)");
                    if (matchWxH.Success)
                    {
                        int pixelWidth = int.Parse(matchWxH.Groups[1].Value);
                        int pixelHeight = int.Parse(matchWxH.Groups[2].Value);

                        // 根據像素寬度計算每行需要的位元組數
                        // (pixelWidth + 7) / 8 是一種向上取整的技巧
                        int bytesPerRow = (pixelWidth + 7) / 8;

                        // 找到就直接返回，這是最可靠的
                        return (bytesPerRow, pixelHeight);
                    }

                    // 原有的邏輯 (第二優先級)：尋找 "Char Width:" 和 "Char Height:"
                    if (line.Contains("// Char Width:"))
                    {
                        int.TryParse(line.Split(':')[1].Trim(), out int width);
                        charWidthFromComment = width;
                    }
                    else if (line.Contains("// Char Height:"))
                    {
                        int.TryParse(line.Split(':')[1].Trim(), out int height);
                        charHeightFromComment = height;
                    }
                }

                // 如果 "Char Width/Height" 格式被找到，則使用它們
                if (charWidthFromComment.HasValue && charHeightFromComment.HasValue)
                {
                    int bytesPerRow = (charWidthFromComment.Value + 7) / 8;
                    return (bytesPerRow, charHeightFromComment.Value);
                }
            }

            // =========================================================================
            // 如果註解中沒有，再進行第二遍掃描：解析二維陣列宣告 [H][W]
            // =========================================================================
            using (StringReader sr = new StringReader(content))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains("const uint8_t"))
                    {
                        var matchXY = Regex.Match(line, @"\[(\d+)\]\[(\d+)\]");
                        if (matchXY.Success)
                        {
                            int height = int.Parse(matchXY.Groups[1].Value);
                            int bytesPerRow = int.Parse(matchXY.Groups[2].Value);
                            return (bytesPerRow, height);
                        }
                    }
                }
            }

            // 如果所有方法都失敗，才返回 (0,0)
            return (0, 0);
        }


        /// <summary>
        /// 從內容中解析單一字元的點陣資料到 CharBitmapData 物件。
        /// </summary>
        private CharBitmapData? ParseCharBitmapData(string content)
        {
            (int bytesPerRow, int height) = GetArrayNumericDimensions(content); // 取得位元組寬和高
            if (bytesPerRow == 0 || height == 0)
            {
                return null; // 無法獲取維度
            }

            CharBitmapData charData = new CharBitmapData(bytesPerRow, height);
            bool insideArray = false;
            int bracketCount = 0;

            foreach (string line in content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None))
            {
                if (line.Contains("const uint8_t") && line.Contains("{"))
                {
                    insideArray = true;
                    int dataStart = line.IndexOf('{');
                    if (dataStart >= 0)
                    {
                        string part = line.Substring(dataStart + 1);
                        bracketCount += part.Count(c => c == '{');
                        bracketCount -= part.Count(c => c == '}');
                        // 這裡要處理單行資料，或者多行資料的開頭
                        // 我們會把所有數據收集起來，然後再分割成行
                    }
                    continue;
                }

                if (insideArray)
                {
                    bracketCount += line.Count(c => c == '{');
                    bracketCount -= line.Count(c => c == '}');

                    // 移除行首尾空格，並移除可能存在的註解
                    string cleanLine = line.Split(new[] { "//" }, StringSplitOptions.None)[0].Trim();

                    // 如果找到 }; 表示陣列結束
                    if (cleanLine.EndsWith("};"))
                    {
                        cleanLine = cleanLine.Substring(0, cleanLine.Length - 2).Trim();
                        insideArray = false; // 陣列結束
                    }
                    else if (cleanLine.EndsWith("}")) // 如果只有 } 沒有 ; (例如巢狀陣列的結尾)
                    {
                        cleanLine = cleanLine.Substring(0, cleanLine.Length - 1).Trim();
                    }

                    if (!string.IsNullOrEmpty(cleanLine))
                    {
                        // 將所有十六進位值解析出來
                        foreach (string hexVal in cleanLine.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (hexVal.StartsWith("0x", StringComparison.OrdinalIgnoreCase) && byte.TryParse(hexVal.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out byte b))
                            {
                                // 這裡需要更複雜的邏輯來將 byte 放入 CharBitmapData.Rows
                                // 因為我們不能直接把所有 byte 放到一個 flat list，然後再分行
                                // 我們需要知道當前是第幾行，並且每行有多少 byte
                                // 這裡暫時先將數據以 flat 的方式收集，之後再分割
                                // 更理想的方法是，這個解析器直接按行解析
                            }
                        }
                    }

                    if (!insideArray && bracketCount <= 0) // 陣列實際結束
                        break;
                }
            }

            // 重新設計 ParseCharBitmapData 的解析邏輯，使其按行解析
            // 這是一個更直接的解析方法：
            List<byte[]> rows = new List<byte[]>();
            List<byte> currentRowBytes = new List<byte>();
            int currentBytesInRow = 0;

            // 我們需要從內容中精確地提取原始數據行
            // 先找到整個數據區塊
            string dataBlock = "";
            bool inDataBlock = false;
            int dataBlockBracketCount = 0;

            foreach (string line in content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None))
            {
                if (line.Contains("const uint8_t") && line.Contains("{"))
                {
                    inDataBlock = true;
                    int startIdx = line.IndexOf('{');
                    dataBlockBracketCount += line.Count(c => c == '{');
                    dataBlockBracketCount -= line.Count(c => c == '}');
                    if (startIdx >= 0) dataBlock += line.Substring(startIdx + 1);
                    continue;
                }
                if (inDataBlock)
                {
                    dataBlockBracketCount += line.Count(c => c == '{');
                    dataBlockBracketCount -= line.Count(c => c == '}');

                    if (dataBlockBracketCount <= 0 && line.Contains("};")) // 找到結尾
                    {
                        int endIdx = line.LastIndexOf("};");
                        if (endIdx >= 0) dataBlock += line.Substring(0, endIdx);
                        inDataBlock = false;
                        break;
                    }
                    else
                    {
                        dataBlock += line;
                    }
                }
            }

            // 清理和分割數據塊
            dataBlock = dataBlock.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
            string[] allHexValues = dataBlock.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string hexValStr in allHexValues)
            {
                if (hexValStr.StartsWith("0x", StringComparison.OrdinalIgnoreCase) && byte.TryParse(hexValStr.Substring(2), System.Globalization.NumberStyles.HexNumber, null, out byte b))
                {
                    currentRowBytes.Add(b);
                    currentBytesInRow++;

                    if (currentBytesInRow == bytesPerRow)
                    {
                        rows.Add(currentRowBytes.ToArray());
                        currentRowBytes.Clear();
                        currentBytesInRow = 0;
                    }
                }
            }

            // 如果還有剩餘的字節，但不足一行 (這不應該發生在正常格式中，但為安全起見處理)
            if (currentRowBytes.Any())
            {
                // 填充剩餘字節為 0x00，直到滿 bytesPerRow
                while (currentRowBytes.Count < bytesPerRow)
                {
                    currentRowBytes.Add(0x00);
                }
                rows.Add(currentRowBytes.ToArray());
            }

            // 檢查解析到的行數是否符合預期高度
            if (rows.Count != height)
            {
                // 這裡可以選擇拋出異常、警告或調整高度
                // 為了確保後續處理正確，這裡應該進行嚴格檢查或修正
                // 目前先返回 null 表示解析失敗或不匹配
                // MessageBox.Show($"警告：檔案解析到的行數 ({rows.Count}) 與宣告的高度 ({height}) 不匹配。");
                // return null;
                // 這裡我們信任 GetArrayNumericDimensions，如果解析行數不符，則強行填充或截斷
                while (rows.Count < height)
                {
                    rows.Add(Enumerable.Repeat((byte)0x00, bytesPerRow).ToArray()); // 填充缺失的行
                }
                while (rows.Count > height)
                {
                    rows.RemoveAt(rows.Count - 1); // 截斷多餘的行
                }
            }

            charData.Rows = rows;
            return charData;
        }

        private string ExtractMeta(string content, string key)
        {
            using (StringReader sr = new StringReader(content))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(key))
                    {
                        int idx = line.IndexOf(':');
                        if (idx >= 0 && idx + 1 < line.Length)
                            return line.Substring(idx + 1).Trim();
                    }
                }
            }
            return string.Empty;
        }

        // 一個私有的輔助方法，專門處理單一字元的生成
        private string GenerateSingleCharArray(char c, FontGenerationOptions options, int maxWidth, int maxHeight)
        {
            // ... 這裡就是您所有複雜的 GDI+ 繪圖和像素讀取程式碼 ...
            // 為了範例，我們回傳一個假的陣列
            return $"const uint8_t font_char_{(int)c}[{maxHeight}][{maxWidth}] = {{ /* data for '{c}' */ }};";
        }
    }
}