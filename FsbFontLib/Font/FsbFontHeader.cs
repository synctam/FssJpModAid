namespace FsbFontLib.Font
{
    using System.IO;
    using System.Text;
    using FsbCommonLib;
    using FsbFontLib.BMFont;

    /// <summary>
    /// フォント座標ヘッダー
    /// </summary>
    public class FsbFontHeader
    {
        public int GameObjectFileID { get; private set; }

        public long GameObjectPathID { get; private set; }

        public bool GameObjectEnables { get; private set; }

        public int ScriptFileID { get; private set; }

        public long ScriptPathID { get; private set; }

        public string ScriptName { get; private set; }

        public int MaterialFileID { get; private set; }

        public long MaterialPathID { get; private set; }

        public float UVRectX { get; private set; }

        public float UVRectY { get; private set; }

        public float UVRectWidth { get; private set; }

        public float UVRectHeight { get; private set; }

        public int BMFontSize { get; private set; }

        public int BMFontBase { get; private set; }

        public int BMFontWidth { get; private set; }

        public int BMFontHeight { get; private set; }

        public string SpriteName { get; private set; }

        /// <summary>
        /// Streamからフォント座標ヘッダーを読み込む。
        /// </summary>
        /// <param name="reader">Stream</param>
        public void Read(BinaryReader reader)
        {
            this.GameObjectFileID = reader.ReadInt32();
            this.GameObjectPathID = reader.ReadInt64();
            this.GameObjectEnables = FsbBinUtils.ReadBoolean(reader);

            this.ScriptFileID = reader.ReadInt32();
            this.ScriptPathID = reader.ReadInt64();
            this.ScriptName = FsbBinUtils.ReadString(reader);

            this.MaterialFileID = reader.ReadInt32();
            this.MaterialPathID = reader.ReadInt64();

            this.UVRectX = reader.ReadSingle();
            this.UVRectY = reader.ReadSingle();
            this.UVRectWidth = reader.ReadSingle();
            this.UVRectHeight = reader.ReadSingle();

            this.BMFontSize = reader.ReadInt32();
            this.BMFontBase = reader.ReadInt32();
            this.BMFontWidth = reader.ReadInt32();
            this.BMFontHeight = reader.ReadInt32();

            this.SpriteName = FsbBinUtils.ReadString(reader);
        }

        /// <summary>
        /// Streamへフォント座標ヘッダーを書き込む。
        /// </summary>
        /// <param name="writer">Stream</param>
        public void Write(BinaryWriter writer)
        {
            //// ToDo: ファイル名や内部名の長さを求める処理を追加。
            writer.Write(this.GameObjectFileID);
            writer.Write(this.GameObjectPathID);
            FsbBinUtils.WriteBoolean(writer, this.GameObjectEnables);

            writer.Write(this.ScriptFileID);
            writer.Write(this.ScriptPathID);
            FsbBinUtils.WriteString(writer, this.ScriptName);

            writer.Write(this.MaterialFileID);
            writer.Write(this.MaterialPathID);

            writer.Write(this.UVRectX);
            writer.Write(this.UVRectY);
            writer.Write(this.UVRectWidth);
            writer.Write(this.UVRectHeight);

            writer.Write(this.BMFontSize);
            writer.Write(this.BMFontBase);
            writer.Write(this.BMFontWidth);
            writer.Write(this.BMFontHeight);

            FsbBinUtils.WriteString(writer, this.SpriteName);
        }

        /// <summary>
        /// フォント座標ヘッダーから値を設定する。
        /// </summary>
        /// <param name="header">フォント座標ヘッダー</param>
        public void SetHeader(FsbFontHeader header)
        {
            this.BMFontSize = header.BMFontSize;
            this.BMFontBase = header.BMFontBase;
            this.BMFontWidth = header.BMFontWidth;
            this.BMFontHeight = header.BMFontHeight;

            this.GameObjectFileID = header.GameObjectFileID;
            this.GameObjectPathID = header.GameObjectPathID;
            this.GameObjectEnables = header.GameObjectEnables;

            this.MaterialFileID = header.MaterialFileID;
            this.MaterialPathID = header.MaterialPathID;

            this.ScriptFileID = header.ScriptFileID;
            this.ScriptPathID = header.ScriptPathID;
            this.ScriptName = header.ScriptName;

            this.SpriteName = header.SpriteName;

            this.UVRectX = header.UVRectX;
            this.UVRectY = header.UVRectY;
            this.UVRectWidth = header.UVRectWidth;
            this.UVRectHeight = header.UVRectHeight;
        }

        /// <summary>
        /// 新しいフォントマップから変換する。
        /// </summary>
        /// <param name="jp">新しいフォントマップ</param>
        public void Convert(FsbBMFontMap jp)
        {
            //// 必要最低限の情報のみ変換する。
            this.BMFontSize = jp.FontSize;
            this.BMFontBase = jp.FontBase;
            this.BMFontWidth = jp.ImageWidth;
            this.BMFontHeight = jp.ImageHeight;

            ////this.GameObjectFileID
            ////this.GameObjectPathID
            ////this.GameObjectEnables

            ////this.MaterialFileID
            ////this.MaterialPathID

            ////this.ScriptFileID
            ////this.ScriptPathID
            ////this.ScriptNameLength
            ////this.ScriptName

            ////this.SpriteNameLength
            ////this.SpriteName

            ////this.UVRectX
            ////this.UVRectY
            ////this.UVRectWidth
            ////this.UVRectHeight
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns>Cloned Header</returns>
        public FsbFontHeader Clone()
        {
            var fsbFontHeader = new FsbFontHeader();

            fsbFontHeader.BMFontSize = this.BMFontWidth;
            fsbFontHeader.BMFontBase = this.BMFontBase;
            fsbFontHeader.BMFontWidth = this.BMFontWidth;
            fsbFontHeader.BMFontHeight = this.BMFontHeight;

            fsbFontHeader.GameObjectFileID = this.GameObjectFileID;
            fsbFontHeader.GameObjectPathID = this.GameObjectPathID;
            fsbFontHeader.GameObjectEnables = this.GameObjectEnables;

            fsbFontHeader.MaterialFileID = this.MaterialFileID;
            fsbFontHeader.MaterialPathID = this.MaterialPathID;

            fsbFontHeader.ScriptFileID = this.ScriptFileID;
            fsbFontHeader.ScriptPathID = this.ScriptPathID;
            fsbFontHeader.ScriptName = this.ScriptName;

            fsbFontHeader.SpriteName = this.SpriteName;

            fsbFontHeader.UVRectX = this.UVRectX;
            fsbFontHeader.UVRectY = this.UVRectY;
            fsbFontHeader.UVRectWidth = this.UVRectWidth;
            fsbFontHeader.UVRectHeight = this.UVRectHeight;

            return fsbFontHeader;
        }

        /// <summary>
        /// デバッグ用テキストを返す。
        /// </summary>
        /// <returns>デバッグ用テキスト</returns>
        public override string ToString()
        {
            var buff = new StringBuilder();

            buff.AppendLine(
                $"GameObjectFileID({this.GameObjectFileID}) " +
                $"GameObjectPathID({this.GameObjectPathID}) " +
                $"Enables({this.GameObjectEnables})");
            buff.AppendLine(
                $"  ScriptFileID({this.ScriptFileID}) " +
                $"ScriptPathID({this.ScriptPathID}) " +
                $"ScriptName({this.ScriptName})");
            buff.AppendLine(
                $"  MaterialFileID({this.MaterialFileID}) " +
                $"MaterialPathID({this.MaterialPathID})");
            buff.AppendLine(
                $"  UVRectX({this.UVRectX}) " +
                $"UVRectY({this.UVRectY}) " +
                $"UVRectWidth({this.UVRectWidth}) " +
                $"UVRectHeight({this.UVRectHeight})");
            buff.AppendLine(
                $"  BMFontSize({this.BMFontSize}) " +
                $"BMFontBase({this.BMFontBase}) " +
                $"BMFontWidth({this.BMFontWidth}) " +
                $"BMFontHeight({this.BMFontHeight})");
            buff.AppendLine($"  ScriptName({this.SpriteName})");

            return buff.ToString();
        }
    }
}
