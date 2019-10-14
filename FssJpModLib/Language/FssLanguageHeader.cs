namespace FssJpModLib.Language
{
    using System;
    using System.IO;
    using FsbCommonLib;

    /// <summary>
    /// 言語ヘッダー
    /// </summary>
    public class FssLanguageHeader
    {
        public int GameObjectFileID { get; private set; }

        public long GameObjectPathID { get; private set; }

        public bool Enabled { get; private set; }

        public int ScriptFileID { get; private set; }

        public long ScriptPathID { get; private set; }

        public string Name { get; private set; }

        public string  GoogleWebServiceURL { get; private set; }

        public string GoogleSpreadsheetKey { get; private set; }

        public string GoogleSpreadsheetName { get; private set; }

        public string GoogleLastUpdatedVersion { get; private set; }

        public int GoogleUpdateFrequency { get; private set; }

        /// <summary>
        /// Streamからデータを読み込み、言語ヘッダーを返す。
        /// </summary>
        /// <param name="br">Stream</param>
        /// <returns>言語ヘッダー</returns>
        public static FssLanguageHeader Read(BinaryReader br)
        {
            var fssLanguageHeader = new FssLanguageHeader();

            fssLanguageHeader.GameObjectFileID = br.ReadInt32();
            fssLanguageHeader.GameObjectPathID = br.ReadInt64();
            fssLanguageHeader.Enabled = FsbBinUtils.ReadBoolean(br);
            fssLanguageHeader.ScriptFileID = br.ReadInt32();
            fssLanguageHeader.ScriptPathID = br.ReadInt64();
            fssLanguageHeader.Name = FsbBinUtils.ReadString(br);
            fssLanguageHeader.GoogleWebServiceURL  = FsbBinUtils.ReadString(br);
            fssLanguageHeader.GoogleSpreadsheetKey  = FsbBinUtils.ReadString(br);
            fssLanguageHeader.GoogleSpreadsheetName  = FsbBinUtils.ReadString(br);
            fssLanguageHeader.GoogleLastUpdatedVersion  = FsbBinUtils.ReadString(br);
            fssLanguageHeader.GoogleUpdateFrequency = br.ReadInt32();

            return fssLanguageHeader;
        }

        /// <summary>
        /// Streamにデータを書き込む。
        /// </summary>
        /// <param name="bw">Stream</param>
        public void Write(BinaryWriter bw)
        {
            bw.Write(this.GameObjectFileID);
            bw.Write(this.GameObjectPathID);
            FsbBinUtils.WriteBoolean(bw, this.Enabled);
            bw.Write(this.ScriptFileID);
            bw.Write(this.ScriptPathID);
            FsbBinUtils.WriteString(bw, this.Name);
            FsbBinUtils.WriteString(bw, this.GoogleWebServiceURL);
            FsbBinUtils.WriteString(bw, this.GoogleSpreadsheetKey);
            FsbBinUtils.WriteString(bw, this.GoogleSpreadsheetName);
            FsbBinUtils.WriteString(bw, this.GoogleLastUpdatedVersion);
            bw.Write(this.GoogleUpdateFrequency);
        }

        /// <summary>
        /// 自分自身のクローンを返す。
        /// </summary>
        /// <returns>自分自身のクローン</returns>
        public FssLanguageHeader Clone()
        {
            var languageHeader = new FssLanguageHeader()
            {
                GameObjectFileID = this.GameObjectFileID,
                GameObjectPathID = this.GameObjectPathID,
                Enabled = this.Enabled,
                ScriptFileID = this.ScriptFileID,
                ScriptPathID = this.ScriptPathID,
                Name = this.Name,
                GoogleWebServiceURL = this.GoogleWebServiceURL,
                GoogleSpreadsheetKey = this.GoogleSpreadsheetKey,
                GoogleSpreadsheetName = this.GoogleSpreadsheetName,
                GoogleLastUpdatedVersion = this.GoogleLastUpdatedVersion,
                GoogleUpdateFrequency = this.GoogleUpdateFrequency,
            };

            return languageHeader;
        }
    }
}
