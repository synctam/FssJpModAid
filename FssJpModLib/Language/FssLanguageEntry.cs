namespace FssJpModLib.Language
{
    using System.Collections.Generic;
    using System.IO;
    using FsbCommonLib;

    /// <summary>
    /// 言語エントリー
    /// </summary>
    public class FssLanguageEntry
    {
        /// <summary>
        /// 言語エントリーID
        /// </summary>
        public string EntryID { get; private set; } = string.Empty;

        /// <summary>
        /// 各国の言語テキストのリスト
        /// </summary>
        public List<string> Texts { get; private set; } = null;

        /// <summary>
        /// TermType
        /// </summary>
        public int TermType { get; private set; }

        /// <summary>
        /// 説明
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Touch
        /// </summary>
        public List<string> LanguagesTouch { get; private set; } = null;

        /// <summary>
        /// Flags
        /// </summary>
        public List<int> Flags { get; private set; } = null;

        /// <summary>
        /// Streamからデータを読み込み、言語エントリーを返す。
        /// </summary>
        /// <param name="reader">Stream</param>
        /// <returns>言語エントリー</returns>
        public static FssLanguageEntry Read(BinaryReader reader)
        {
            var fssLanguageEntry = new FssLanguageEntry();

            fssLanguageEntry.EntryID = FsbBinUtils.ReadString(reader);
            fssLanguageEntry.TermType = reader.ReadInt32();
            fssLanguageEntry.Description = FsbBinUtils.ReadString(reader);
            if (fssLanguageEntry.Description == "\"")
            {
                ////fssLanguageEntry.Description = string.Empty;
            }

            var langCount = reader.ReadInt32();
            fssLanguageEntry.Texts = new List<string>();
            for (var i = 0; i < langCount; i++)
            {
                var text = FsbBinUtils.ReadString(reader);
                fssLanguageEntry.Texts.Add(text);
            }

            int languagesTouchCount = reader.ReadInt32();
            fssLanguageEntry.LanguagesTouch = new List<string>();
            for (int i = 0; i < languagesTouchCount; i++)
            {
                var touch = FsbBinUtils.ReadString(reader);
                fssLanguageEntry.LanguagesTouch.Add(touch);
            }

            int flagsCount = reader.ReadInt32();
            fssLanguageEntry.Flags = new List<int>();
            for (int i = 0; i < flagsCount; i++)
            {
                var flags = reader.ReadInt32();
                fssLanguageEntry.Flags.Add(flags);
            }

            return fssLanguageEntry;
        }

        /// <summary>
        /// Streamにデータを書き込む。
        /// </summary>
        /// <param name="bw">Stream</param>
        public void Write(BinaryWriter bw)
        {
            FsbBinUtils.WriteString(bw, this.EntryID);
            bw.Write(this.TermType);
            FsbBinUtils.WriteString(bw, this.Description);

            //// Texts
            bw.Write(this.Texts.Count);
            foreach (var text in this.Texts)
            {
                FsbBinUtils.WriteString(bw, text);
            }

            //// LanguagesTouch
            bw.Write(this.LanguagesTouch.Count);
            foreach (var entry in this.LanguagesTouch)
            {
                FsbBinUtils.WriteString(bw, entry);
            }

            //// Flags
            bw.Write(this.Flags.Count);
            foreach (var entry in this.Flags)
            {
                bw.Write(entry);
            }
        }

        /// <summary>
        /// 自分自身のクローンを返す。
        /// </summary>
        /// <returns>自分自身のクローン</returns>
        public FssLanguageEntry Clone()
        {
            var entry = new FssLanguageEntry();

            entry.Description = this.Description;
            entry.EntryID = this.EntryID;
            entry.Flags = new List<int>();
            foreach (var flag in this.Flags)
            {
                entry.Flags.Add(flag);
            }

            entry.LanguagesTouch = this.LanguagesTouch;
            entry.TermType = this.TermType;

            entry.Texts = new List<string>();
            foreach (var text in this.Texts)
            {
                entry.Texts.Add(text);
            }

            return entry;
        }
    }
}
