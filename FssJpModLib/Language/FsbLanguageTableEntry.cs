namespace FssJpModLib.Language
{
    using System;
    using System.IO;
    using FsbCommonLib;

    /// <summary>
    /// 言語テーブルエントリー
    /// </summary>
    public class FsbLanguageTableEntry
    {
        public FsbLanguageTableEntry() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="reader">Stream</param>
        public FsbLanguageTableEntry(BinaryReader reader)
        {
            this.Read(reader);
        }

        /// <summary>
        /// 言語区分の表示名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 言語区分。
        /// string.emptyの場合あり(例：ロシア語)。
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Streamから言語テーブルエントリーを読み込む。
        /// </summary>
        /// <param name="br">Stream</param>
        public void Read(BinaryReader br)
        {
            this.Name = FsbBinUtils.ReadString(br);
            this.Code = FsbBinUtils.ReadString(br);
        }

        /// <summary>
        /// Streamに言語テーブルエントリーを書き込む。
        /// </summary>
        /// <param name="bw">Stream</param>
        public void Write(BinaryWriter bw)
        {
            FsbBinUtils.WriteString(bw, this.Name);
            FsbBinUtils.WriteString(bw, this.Code);
        }

        /// <summary>
        /// 自分自身のクローンを返す。
        /// </summary>
        /// <returns>自分自身のクローン</returns>
        public FsbLanguageTableEntry Clone()
        {
            var tableEntry = new FsbLanguageTableEntry();

            tableEntry.Name = this.Name;
            tableEntry.Code = this.Code;

            return tableEntry;
        }
    }
}
