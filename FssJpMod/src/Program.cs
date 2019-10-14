namespace FssJpMod
{
    using System;
    using FssJpModLib.Language;
    using FssJpModLib.Translation;
    using FssJpModLib.TransSheet;
    using MonoOptions;
    using S5mDebugTools;

    public class Program
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

                MakeMod(opt.Arges);

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

        private static void MakeMod(TOptions.TArgs opt)
        {
            //// 言語情報ファイルの読み込み。
            var languageInfoEn = FssLanguageDao.LoadFromFile(opt.FileNameInput);
            //// 翻訳シートの読み込み。
            var sheetFile = FssTransSheetDao.LoadFromCsv(opt.FileNameSheet);
            //// 翻訳シートを使用し、日本語化する。
            var languageInfoJp = FssLanguageTranslation.Translate(
                languageInfoEn, sheetFile, opt.UseMachineTrans, FssLanguageInfo.LanguageNo.English);
            //// 日本語化した言語情報を言語情報ファイルとして出力する。
            FssLanguageDao.SaveToFile(languageInfoJp, opt.FileNameOutput);
        }
    }
}
