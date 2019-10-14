namespace FsbFontChanger
{
    using System;
    using System.IO;
    using System.Text;
    using FsbFontLib.BMFont;
    using FsbFontLib.Convert;
    using FsbFontLib.Font;
    using MonoOptions;
    using S5mDebugTools;

    internal class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                var opt = new TOptions(args);
                if (opt.IsError)
                {
                    TDebugUtils.Pause();
                    return 1;
                }

                if (opt.Arges.Help)
                {
                    opt.ShowUsage();

                    TDebugUtils.Pause();
                    return 1;
                }

                MakeJpFont(opt.Arges);

                TDebugUtils.Pause();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();

                TDebugUtils.Pause();
                return 1;
            }
        }

        private static void MakeJpFont(TOptions.TArgs opt)
        {
            Encoding enc = new UTF8Encoding(false);

            //// オリジナルの座標情報を読み込む。
            var orgFontMap = FsbFontDao.Load(opt.FileNameInput);

            //// 日本語版BMFontの座標情報(XML)を読み込む
            var bmfontMap = FsbBMFontDao.Load(opt.FileNameFontXml);

            var replacedList = new StringBuilder();

            //// オリジナルと日本語版座標情報をマージする。
            var newFont = FsbFontConvert.Merge(orgFontMap, bmfontMap, replacedList, 0, 0);

            //// 新しい座標情報ファイルを書き出す。
            FsbFontDao.Save(opt.FileNameOutput, newFont);
        }

        /// <summary>
        /// Debug用：フォントの座標情報をテキスト形式で出力する。
        /// </summary>
        private static void FontCheckList()
        {
            var enc = new UTF8Encoding(false);

            //// フォントの座標情報を読み込む。
            var orgFontMap = FsbFontDao.Load(@"data\OriginalMap\resources_00001.114");
            //// フォントの座標情報の確認リストを出力
            File.WriteAllText(@"resources_00001.114.txt", orgFontMap.ToString(), enc);
        }
    }
}
