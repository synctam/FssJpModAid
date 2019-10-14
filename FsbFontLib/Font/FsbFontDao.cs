namespace FsbFontLib.Font
{
    using System.IO;
    using System.Text;

    public class FsbFontDao
    {
        /// <summary>
        /// フォント座標ファイルを読み込み、フォント座標情報を返す。
        /// </summary>
        /// <param name="path">フォント座標ファイルのパス</param>
        /// <returns>フォント座標情報</returns>
        public static FsbFont Load(string path)
        {
            var fsbFont = new FsbFont();

            using (var br = new BinaryReader(File.OpenRead(path), Encoding.UTF8))
            {
                fsbFont.Header.Read(br);
                fsbFont.FontEntries.Read(br);
                fsbFont.Footer.Read(br);
            }

            return fsbFont;
        }

        /// <summary>
        /// フォント座標情報をファイルに書き出す。
        /// </summary>
        /// <param name="path">フォント座標ファイルのパス</param>
        /// <param name="fsbFont">フォント座標情報</param>
        public static void Save(string path, FsbFont fsbFont)
        {
            using (var writer = new BinaryWriter(File.OpenWrite(path), Encoding.UTF8))
            {
                fsbFont.Header.Write(writer);
                fsbFont.FontEntries.Write(writer);
                fsbFont.Footer.Write(writer);
            }
        }
    }
}
