namespace FssJpModLib.Language
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// 言語ファイル
    /// </summary>
    public class FssLanguageFile
    {
        /// <summary>
        /// 言語エントリーの辞書。キーはEntryID。
        /// </summary>
        public IDictionary<string, FssLanguageEntry> Items { get; } = new Dictionary<string, FssLanguageEntry>();

        /// <summary>
        /// Streamからデータを読み込み、言語ファイルを返す。
        /// </summary>
        /// <param name="reader">Stream</param>
        /// <returns>言語ファイル</returns>
        public static FssLanguageFile Read(BinaryReader reader)
        {
            var fssLanguageFile = new FssLanguageFile();

            int entryCount = reader.ReadInt32();
            for (int i = 0; i < entryCount; i++)
            {
                var fssLanguageEntry = FssLanguageEntry.Read(reader);
                fssLanguageFile.AddEntry(fssLanguageEntry);
            }

            return fssLanguageFile;
        }

        /// <summary>
        /// Streamにデータを書き込む。
        /// </summary>
        /// <param name="bw">Stream</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.Items.Count);
            foreach (var entry in this.Items.Values)
            {
                entry.Write(bw);
            }
        }

        /// <summary>
        /// 言語エントリーを追加する。
        /// </summary>
        /// <param name="fssLanguageEntry">言語エントリー</param>
        public void AddEntry(FssLanguageEntry fssLanguageEntry)
        {
            if (this.Items.ContainsKey(fssLanguageEntry.EntryID))
            {
                var msg = $"Duplicate EntryID({fssLanguageEntry.EntryID}).";
                throw new Exception(msg);
            }
            else
            {
                this.Items.Add(fssLanguageEntry.EntryID, fssLanguageEntry);
            }
        }
    }
}
