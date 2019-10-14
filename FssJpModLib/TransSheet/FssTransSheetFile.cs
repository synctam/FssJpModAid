namespace FssJpModLib.TransSheet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FssTransSheetFile
    {
        /// <summary>
        /// 翻訳シートエントリーの辞書。
        /// キーは、EntryID。
        /// </summary>
        public Dictionary<string, FssTransSheetEntry> Items { get; } =
            new Dictionary<string, FssTransSheetEntry>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// 翻訳シートエントリーの追加。
        /// </summary>
        /// <param name="entry">翻訳シートエントリー</param>
        public void AddEntry(FssTransSheetEntry entry)
        {
            if (this.Items.ContainsKey(entry.EntryID))
            {
                throw new Exception($"Duplicate EntryID({entry.EntryID}).");
            }
            else
            {
                this.Items.Add(entry.EntryID, entry);
            }
        }

        /// <summary>
        /// 翻訳結果を返す。
        /// </summary>
        /// <param name="entryID">エントリーID</param>
        /// <param name="textEn">原文</param>
        /// <param name="useMT">機械翻訳の使用有無</param>
        /// <returns>翻訳結果</returns>
        public string Translate(string entryID, string textEn, bool useMT)
        {
            //// en | jp | mt | result
            ////  o |  o |  o | 7: jp
            ////  o |  o |  x | 6: jp
            ////  o |  x |  o | 5: mt <> en
            ////  o |  x |  x | 4: en
            ////  x |  o |  o | 3: en
            ////  x |  o |  x | 2: en
            ////  x |  x |  o | 1: en
            ////  x |  x |  x | 0: en
            if (this.Items.ContainsKey(entryID))
            {
                var sheetEntry = this.Items[entryID];
                var e = !string.IsNullOrWhiteSpace(textEn);
                var j = !string.IsNullOrWhiteSpace(sheetEntry.LangJP);
                var m = !string.IsNullOrWhiteSpace(sheetEntry.LangMT);
                if (e && j && m) //// 7
                {
                    return sheetEntry.LangJP;
                }
                else if (e && j && !m) //// 6
                {
                    return sheetEntry.LangJP;
                }
                else if (e && !j && m) //// 5
                {
                    if (useMT)
                    {
                        if (textEn.Contains('{'))
                        {
                            //// 変数記号が含まれるデータは除外する。
                            return textEn;
                        }
                        else
                        {
                            return sheetEntry.LangMT;
                        }
                    }
                    else
                    {
                        return textEn;
                    }
                }
                else if (e && !j && !m) //// 4
                {
                    return textEn;
                }
                else if (!e && j && m) //// 3
                {
                    return textEn;
                }
                else if (!e && j && !m) //// 2
                {
                    return textEn;
                }
                else if (!e && !j && m) //// 1
                {
                    return textEn;
                }
                else if (!e && !j && !m) //// 0
                {
                    return textEn;
                }
                else
                {
                    throw new Exception($"logic error");
                }
            }
            else
            {
                //// 翻訳シートにエントリーが存在しない。
                return textEn;
            }
        }

        /// <summary>
        /// 指定されたEntryIDの翻訳シートエントリーを返す。
        /// </summary>
        /// <param name="entryID">EntryID</param>
        /// <returns>翻訳シートエントリー</returns>
        public FssTransSheetEntry GetEntry(string entryID)
        {
            if (this.Items.ContainsKey(entryID))
            {
                return this.Items[entryID];
            }
            else
            {
                //// 存在しない場合はnullオブジェクトを返す。
                return new FssTransSheetEntry();
            }
        }
    }
}
