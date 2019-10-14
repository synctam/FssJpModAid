namespace FssJpModLib.Language
{
    using System;
    using System.IO;
    using System.Text;
    using FsbCommonLib.FileUtils;

    /// <summary>
    /// 言語情報入出力
    /// </summary>
    public class FssLanguageDao
    {
        /// <summary>
        /// 言語情報ファイルを読み込み、言語情報を返す。
        /// </summary>
        /// <param name="path">言語情報ファイル</param>
        /// <returns>言語情報</returns>
        public static FssLanguageInfo LoadFromFile(string path)
        {
            var fssLanguageInfo = new FssLanguageInfo();

            using (var reader = new BinaryReader(File.OpenRead(path), Encoding.UTF8))
            {
                var fssLanguageHeader = FssLanguageHeader.Read(reader);
                var fssLanguageFile = FssLanguageFile.Read(reader);
                var fssLanguageFooter = FssLanguageFooter.Read(reader);

                fssLanguageInfo.SetHeader(fssLanguageHeader);
                fssLanguageInfo.SetFile(fssLanguageFile);
                fssLanguageInfo.SetFooter(fssLanguageFooter);
            }

            return fssLanguageInfo;
        }

        /// <summary>
        /// 言語情報を言語情報ファイルのパスに書き出す。
        /// </summary>
        /// <param name="languageInfo">言語情報</param>
        /// <param name="path">言語情報ファイルのパス</param>
        public static void SaveToFile(FssLanguageInfo languageInfo, string path)
        {
            //// フォルダーが存在しない場合は作成する。
            var langFolder = Path.GetDirectoryName(path);
            FssFileUtils.SafeCreateDirectory(langFolder);

            using (var sw = new StreamWriter(path, false))
            {
                var bw = new BinaryWriter(sw.BaseStream, Encoding.UTF8);
                languageInfo.LanguageHeader.Write(bw);
                languageInfo.LanguageFile.Write(bw);
                languageInfo.LanguageFooter.Write(bw);
            }
        }
    }
}
