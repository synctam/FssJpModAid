namespace FssJpModLib.TransSheet
{
    using System.IO;
    using System.Text;
    using CsvHelper;
    using FsbCommonLib.FileUtils;
    using FssJpModLib.Language;

    /// <summary>
    /// 翻訳シート入出力
    /// </summary>
    public class FssTransSheetDao
    {
        public static FssTransSheetFile LoadFromCsv(string path, Encoding enc = null)
        {
            if (enc == null)
            {
                enc = Encoding.UTF8;
            }

            using (var reader = new StreamReader(path, enc))
            {
                using (var csv = new CsvReader(reader))
                {
                    //// 区切り文字
                    csv.Configuration.Delimiter = ",";
                    //// ヘッダーの有無
                    csv.Configuration.HasHeaderRecord = true;
                    //// CSVファイルに合ったマッピングルールを登録
                    csv.Configuration.RegisterClassMap<CsvMapper>();
                    //// データを読み出し
                    var records = csv.GetRecords<FssTransSheetEntry>();

                    var sheetFile = new FssTransSheetFile();
                    foreach (var record in records)
                    {
                        var entry = new FssTransSheetEntry()
                        {
                            EntryID = record.EntryID,
                            Hint = record.Hint,
                            LangEN = record.LangEN,
                            LangJP = record.LangJP,
                            LangMT = record.LangMT,
                            Sequence = record.Sequence,
                        };

                        sheetFile.AddEntry(entry);
                    }

                    return sheetFile;
                }
            }
        }

        /// <summary>
        /// 言語情報をCSV形式で保存する。
        /// </summary>
        /// <param name="path">CSVファイルのパス</param>
        /// <param name="langInfo">言語情報</param>
        /// <param name="languageNo">言語番号</param>
        public static void SaveToCsv(
            string path, FssLanguageInfo langInfo, FssLanguageInfo.LanguageNo languageNo)
        {
            var enc = new UTF8Encoding(false);

            //// フォルダーが存在しない場合は作成する。
            var sheetFolder = Path.GetDirectoryName(path);
            FssFileUtils.SafeCreateDirectory(sheetFolder);

            using (var writer = new CsvWriter(new StreamWriter(path, false, enc)))
            {
                writer.Configuration.RegisterClassMap<CsvMapper>();
                writer.WriteHeader<FssTransSheetEntry>();
                writer.NextRecord();

                var data = new FssTransSheetEntry();
                int no = 0;
                foreach (var entry in langInfo.LanguageFile.Items.Values)
                {
                    data.EntryID = entry.EntryID;
                    data.Hint = entry.Description;
                    data.LangEN = entry.Texts[(int)languageNo];
                    data.Sequence = no;

                    writer.WriteRecord(data);
                    writer.NextRecord();

                    no++;
                }
            }
        }

        public class CsvMapper : CsvHelper.Configuration.ClassMap<FssTransSheetEntry>
        {
            public CsvMapper()
            {
                // 出力時の列の順番は指定した順となる。
                this.Map(x => x.Sequence).Name("[[No]]");

                this.Map(x => x.LangEN).Name("[[LangEN]]");
                this.Map(x => x.LangJP).Name("[[LangJP]]");
                this.Map(x => x.LangMT).Name("[[LangMT]]");

                this.Map(x => x.Hint).Name("[[Hint]]");
                this.Map(x => x.EntryID).Name("[[EntryID]]");
            }
        }
    }
}
