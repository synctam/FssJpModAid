namespace FsbFontLib.Font
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// フォント・エントリー
    /// </summary>
    public class FsbFontEntry
    {
        /// <summary>
        /// 文字コード
        /// </summary>
        public int CharacterID { get; set; }

        /// <summary>
        /// X座標
        /// </summary>
        public int PosX { get; set; }

        /// <summary>
        /// Y座標
        /// </summary>
        public int PosY { get; set; }

        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// X軸方向のオフセット
        /// </summary>
        public int OffsetX { get; set; }

        /// <summary>
        /// Y軸方向のオフセット
        /// </summary>
        public int OffsetY { get; set; }

        /// <summary>
        /// 拡張情報
        /// </summary>
        public int AdvanceX { get; set; }

        /// <summary>
        /// チャネル
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// カーニングペアのリスト。
        /// </summary>
        public List<FsbFontKerningPair> Kernings { get; } =
            new List<FsbFontKerningPair>();

        /// <summary>
        /// Streamからフォント・エントリーを読み込む。
        /// </summary>
        /// <param name="reader">Stream</param>
        public void Read(BinaryReader reader)
        {
            //// 基本情報を読み込む。
            this.CharacterID = reader.ReadInt32();
            this.PosX = reader.ReadInt32();
            this.PosY = reader.ReadInt32();
            this.Width = reader.ReadInt32();
            this.Height = reader.ReadInt32();
            this.OffsetX = reader.ReadInt32();
            this.OffsetY = reader.ReadInt32();
            this.AdvanceX = reader.ReadInt32();
            this.Channel = reader.ReadInt32();
            //// カーニングペア情報を読み込む。
            var kerningPairCount = reader.ReadInt32();
            if (kerningPairCount > 0)
            {
                for (int i = 0; i < kerningPairCount / 2; i++)
                {
                    var kp = new FsbFontKerningPair();

                    kp.CharacterID = reader.ReadInt32();
                    kp.Amount = reader.ReadInt32();

                    this.Kernings.Add(kp);
                }
            }
        }

        /// <summary>
        /// Streamにフォント・エントリーを書き込む
        /// </summary>
        /// <param name="writer">Stream</param>
        public void Write(BinaryWriter writer)
        {
            writer.Write(this.CharacterID);
            writer.Write(this.PosX);
            writer.Write(this.PosY);
            writer.Write(this.Width);
            writer.Write(this.Height);
            writer.Write(this.OffsetX);
            writer.Write(this.OffsetY);
            writer.Write(this.AdvanceX);
            writer.Write(this.Channel);
            //// kerning情報は CharacterID, Amount の２項目からなるため、
            //// 書き込む項目数は二倍となる。
            writer.Write(this.Kernings.Count * 2);
            if (this.Kernings.Count > 0)
            {
                foreach (var kerningPair in this.Kernings)
                {
                    writer.Write(kerningPair.CharacterID);
                    writer.Write(kerningPair.Amount);
                }
            }
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns>Cloned Entry</returns>
        public FsbFontEntry Clone()
        {
            var fsbFontEntry = new FsbFontEntry();

            fsbFontEntry.CharacterID = this.CharacterID;
            fsbFontEntry.PosX = this.PosX;
            fsbFontEntry.PosY = this.PosY;
            fsbFontEntry.Width = this.Width;
            fsbFontEntry.Height = this.Height;
            fsbFontEntry.OffsetX = this.OffsetX;
            fsbFontEntry.OffsetY = this.OffsetY;
            fsbFontEntry.AdvanceX = this.AdvanceX;
            fsbFontEntry.Channel = this.Channel;

            foreach (var kernPair in this.Kernings)
            {
                var newKernPair = new FsbFontKerningPair();

                newKernPair.CharacterID = kernPair.CharacterID;
                newKernPair.Amount = kernPair.Amount;

                fsbFontEntry.Kernings.Add(newKernPair);
            }

            return fsbFontEntry;
        }

        /// <summary>
        /// デバッグ用テキストを返す。
        /// </summary>
        /// <returns>デバッグ用テキスト</returns>
        public override string ToString()
        {
            var buff = new StringBuilder();

            buff.AppendLine($"CharacterID({this.CharacterID}[{this.CharacterID:X6}]) X({this.PosX}) Y({this.PosY}) Width({this.Width}) Height({this.Height}) OffsetX({this.OffsetX}) OffsetY({this.OffsetY}) AdvanceX({this.AdvanceX}) Channel({this.Channel}) KerningPairCount({this.Kernings.Count})");
            foreach (var kerningPair in this.Kernings)
            {
                buff.AppendLine(
                    $"    CharacterID({kerningPair.CharacterID}) " +
                    $"Amount({kerningPair.Amount})");
            }

            return buff.ToString();
        }
    }
}