namespace FsbFontLib.Font
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// フォント座標フッター
    /// </summary>
    public class FsbFontFooter
    {
        public int UIAtlasFileID { get; set; }

        public long UIAtlasPathID { get; set; }

        public int UIFontReplacementFileID { get; set; }

        public long UIFontReplacementPathID { get; set; }

        public int SymbolSize { get; set; }

        public int DynamicFontFileID { get; set; }

        public long DynamicFontPathID { get; set; }

        public int DynamicFontSize { get; set; }

        public int DynamicFontStyle { get; set; }

        /// <summary>
        /// Streamからフォント座標フッターを読み込む。
        /// </summary>
        /// <param name="br">Stream</param>
        public void Read(BinaryReader br)
        {
            this.UIAtlasFileID = br.ReadInt32();
            this.UIAtlasPathID = br.ReadInt64();
            this.UIFontReplacementFileID = br.ReadInt32();
            this.UIFontReplacementPathID = br.ReadInt64();
            this.SymbolSize = br.ReadInt32();
            //// ToDo: Symbol の処理を追加する。

            this.DynamicFontFileID = br.ReadInt32();
            this.DynamicFontPathID = br.ReadInt64();

            this.DynamicFontSize = br.ReadInt32();
            this.DynamicFontStyle = br.ReadInt32();
        }

        /// <summary>
        /// Streamにフォント座標フッターを書き込む。
        /// </summary>
        /// <param name="writer">Stream</param>
        public void Write(BinaryWriter writer)
        {
            writer.Write(this.UIAtlasFileID);
            writer.Write(this.UIAtlasPathID);
            writer.Write(this.UIFontReplacementFileID);
            writer.Write(this.UIFontReplacementPathID);
            writer.Write(this.SymbolSize);
            //// ToDo: Symbol の処理を追加する。

            writer.Write(this.DynamicFontFileID);
            writer.Write(this.DynamicFontPathID);
            writer.Write(this.DynamicFontSize);
            writer.Write(this.DynamicFontStyle);
        }

        /// <summary>
        /// フォント座標フッターから値を設定する。
        /// </summary>
        /// <param name="data">フォント座標フッター</param>
        public void SetFooter(FsbFontFooter data)
        {
            this.UIAtlasFileID = data.UIAtlasFileID;
            this.UIAtlasPathID = data.UIAtlasPathID;

            this.UIFontReplacementFileID = data.UIFontReplacementFileID;
            this.UIFontReplacementPathID = data.UIFontReplacementPathID;

            this.SymbolSize = data.SymbolSize;
            //// ToDo: Symbol の処理を追加する。

            this.DynamicFontFileID = data.DynamicFontFileID;
            this.DynamicFontPathID = data.DynamicFontPathID;

            this.DynamicFontSize = data.DynamicFontSize;
            this.DynamicFontStyle = data.DynamicFontStyle;
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns>Cloned Footer</returns>
        public FsbFontFooter Clone()
        {
            FsbFontFooter fsbFontFooter = new FsbFontFooter();

            fsbFontFooter.UIAtlasFileID = this.UIAtlasFileID;
            fsbFontFooter.UIAtlasPathID = this.UIAtlasPathID;

            fsbFontFooter.UIFontReplacementFileID = this.UIFontReplacementFileID;
            fsbFontFooter.UIFontReplacementPathID = this.UIFontReplacementPathID;

            fsbFontFooter.SymbolSize = this.SymbolSize;
            //// ToDo: Symbol の処理を追加する。

            fsbFontFooter.DynamicFontFileID = this.DynamicFontFileID;
            fsbFontFooter.DynamicFontPathID = this.DynamicFontPathID;

            fsbFontFooter.DynamicFontSize = this.DynamicFontSize;
            fsbFontFooter.DynamicFontStyle = this.DynamicFontStyle;

            return fsbFontFooter;
        }

        /// <summary>
        /// デバッグ用テキストを返す。
        /// </summary>
        /// <returns>デバッグ用テキスト</returns>
        public override string ToString()
        {
            StringBuilder buff = new StringBuilder();

            buff.AppendLine(
                $"UIAtlasFileID({this.UIAtlasFileID}) " +
                $"UIAtlasPathID({this.UIAtlasPathID}) " +
                $"UIFontReplacementFileID({this.UIFontReplacementFileID}) " +
                $"UIFontReplacementPathID({this.UIFontReplacementPathID})");
            buff.AppendLine(
                $" SymbolSize({this.SymbolSize}) " +
                $"DynamicFontFileID({this.DynamicFontFileID}) " +
                $"DynamicFontPathID({this.DynamicFontPathID})");
            buff.AppendLine(
                $" DynamicFontSize({this.DynamicFontSize}) " +
                $"DynamicFontStyle({this.DynamicFontStyle})");

            return buff.ToString();
        }
    }
}
