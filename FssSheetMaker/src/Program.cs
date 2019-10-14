namespace FssSheetMaker
{
    using System;
    using FssJpModLib.Language;
    using FssJpModLib.TransSheet;
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

                MakeSheet(opt.Arges);

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

        private static void MakeSheet(TOptions.TArgs opt)
        {
            //// ToDo: 言語番号の処理を追加する。
            var langNo = FssLanguageInfo.LanguageNo.English;
            //// 言語情報ファイルの読み込み。
            var languageInfo = FssLanguageDao.LoadFromFile(opt.FileNameLangInput);
            FssTransSheetDao.SaveToCsv(opt.FileNameSheet, languageInfo, langNo);
        }
    }
}
