namespace FsbFontLib.Font
{
    using System.Text;

    /// <summary>
    /// フォント座標情報
    /// </summary>
    public class FsbFont
    {
        /// <summary>
        /// ヘッダー
        /// </summary>
        public FsbFontHeader Header { get; } = new FsbFontHeader();

        /// <summary>
        /// フォント・エントリー
        /// </summary>
        public FsbFontEntries FontEntries { get; } = new FsbFontEntries();

        /// <summary>
        /// フッター
        /// </summary>
        public FsbFontFooter Footer { get; } = new FsbFontFooter();

        /// <summary>
        /// このオブジェクトのクローンを返す。
        /// </summary>
        /// <returns>クローン</returns>
        public FsbFont Clone()
        {
            var fsbFont = new FsbFont();

            fsbFont.Header.SetHeader(this.Header.Clone());
            fsbFont.FontEntries.SetEntries(this.FontEntries.Clone());
            fsbFont.Footer.SetFooter(this.Footer.Clone());

            return fsbFont;
        }

        /// <summary>
        /// デバッグ用テキストを返す。
        /// </summary>
        /// <returns>デバッグ用テキスト</returns>
        public override string ToString()
        {
            StringBuilder buff = new StringBuilder();
            buff.AppendLine("-------------------------------------------");
            buff.Append(this.Header.ToString());
            buff.AppendLine("-------------------------------------------");
            buff.Append(this.FontEntries.ToString());
            buff.AppendLine("-------------------------------------------");
            buff.Append(this.Footer.ToString());

            return buff.ToString();
        }
    }
}
