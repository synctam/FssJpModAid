namespace FsbCommonLib
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// バイナリーファイル入出力支援クラス
    /// </summary>
    public class FsbBinUtils
    {
        /// <summary>
        /// Binary　streamを読み込み、文字列を返す。
        /// 必要に応じpaddingを読み飛ばす。
        /// </summary>
        /// <param name="br">BinaryReader</param>
        /// <returns>文字列</returns>
        public static string ReadString(BinaryReader br)
        {
            var result = string.Empty;

            var length = br.ReadInt32();
            if (length > 0)
            {
                var scriptNameArray = br.ReadBytes(length);
                result = Encoding.UTF8.GetString(scriptNameArray);
                //// Padding
                ReadPadding(br);
            }

            return result;
        }

        /// <summary>
        /// バイナリー形式で文字列を書き込む。
        /// </summary>
        /// <param name="bw">BinaryWriter</param>
        /// <param name="value">文字列</param>
        public static void WriteString(BinaryWriter bw, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                bw.Write((int)0);
            }
            else
            {
                //// 文字列のバイト数を書き込む
                var length = Encoding.UTF8.GetByteCount(value);
                bw.Write(length);
                //// 文字列を書き込む
                byte[] arrayOfScriptName = Encoding.UTF8.GetBytes(value);
                bw.Write(arrayOfScriptName);
                //// padding処理
                WritePadding(bw);
            }
        }

        /// <summary>
        /// Binary　streamを読み込み、Bool値を返す。
        /// 必要に応じpaddingを読み飛ばす。
        /// </summary>
        /// <param name="br">BinaryReader</param>
        /// <returns>Bool値</returns>
        public static bool ReadBoolean(BinaryReader br)
        {
            var result = br.ReadBoolean();
            //// padding処理
            ReadPadding(br);

            return result;
        }

        /// <summary>
        /// バイナリー形式でbool値を書き込む。
        /// </summary>
        /// <param name="bw">BinaryWriter</param>
        /// <param name="value">bool値</param>
        public static void WriteBoolean(BinaryWriter bw, bool value)
        {
            bw.Write(value);
            //// padding処理
            WritePadding(bw);
        }

        /// <summary>
        /// paddingの書き込み
        /// </summary>
        /// <param name="bw">BinaryWriter</param>
        private static void WritePadding(BinaryWriter bw)
        {
            //// ファイルのオフセット値からpaddingデータを書き込む。
            var amari = bw.BaseStream.Position % 4;
            if (amari > 0)
            {
                var paddingCount = 4 - amari;
                for (int i = 0; i < paddingCount; i++)
                {
                    byte dummy = 0;
                    bw.Write(dummy);
                }
            }
        }

        /// <summary>
        /// Paddingの読み飛ばし
        /// </summary>
        /// <param name="br">BinaryReader</param>
        private static void ReadPadding(BinaryReader br)
        {
            var padmod = (int)(br.BaseStream.Position % 4);
            if (padmod != 0)
            {
                //// paddingの読み飛ばし
                br.ReadBytes(4 - padmod);
            }
        }
    }
}
