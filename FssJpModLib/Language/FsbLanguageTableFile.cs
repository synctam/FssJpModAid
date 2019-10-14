namespace FssJpModLib.Language
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// 言語テーブルファイル
    /// </summary>
    public class FsbLanguageTableFile
    {
        /// <summary>
        /// 言語テーブルエントリーの辞書。
        /// キーは、言語区分の表示名。
        /// </summary>
        public Dictionary<string, FsbLanguageTableEntry> Items { get; } =
            new Dictionary<string, FsbLanguageTableEntry>(StringComparer.CurrentCultureIgnoreCase);

        /// <summary>
        /// 言語テーブルエントリーを追加する。
        /// </summary>
        /// <param name="fsbLanguageCategoryEntry">言語テーブルエントリー</param>
        public void AddEntry(FsbLanguageTableEntry fsbLanguageCategoryEntry)
        {
            if (this.Items.ContainsKey(fsbLanguageCategoryEntry.Name))
            {
                throw new Exception(
                    $"Duplicate language name({fsbLanguageCategoryEntry.Name})");
            }
            else
            {
                this.Items.Add(fsbLanguageCategoryEntry.Name, fsbLanguageCategoryEntry);
            }
        }

        /// <summary>
        /// Streamから言語テーブルエントリーを読み込む。
        /// </summary>
        /// <param name="reader">Stream</param>
        public void Read(BinaryReader reader)
        {
            //// 項目数を読み込む。
            var langCount = reader.ReadInt32();
            for (var i = 0; i < langCount; i++)
            {
                var entry = new FsbLanguageTableEntry(reader);
                this.AddEntry(entry);
            }
        }

        /// <summary>
        /// Streamに言語テーブルエントリーを書き込む。
        /// </summary>
        /// <param name="writer">Stream</param>
        public void Write(BinaryWriter writer)
        {
            //// 項目数を書き込む。
            writer.Write(this.Items.Count);
            foreach (var entry in this.Items.Values)
            {
                entry.Write(writer);
            }
        }

        /// <summary>
        /// 自分自身のクローンを返す。
        /// </summary>
        /// <returns>自分自身のクローン</returns>
        public FsbLanguageTableFile Clone()
        {
            var languageTableFile = new FsbLanguageTableFile();
            foreach (var tableEntry in this.Items.Values)
            {
                languageTableFile.AddEntry(tableEntry.Clone());
            }

            return languageTableFile;
        }
    }
}
