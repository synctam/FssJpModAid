namespace FssJpModLib.Translation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FssJpModLib.Language;
    using FssJpModLib.TransSheet;

    public class FssLanguageTranslation
    {
        public static FssLanguageInfo Translate(
            FssLanguageInfo langageInfoEn, FssTransSheetFile sheetFile, bool useMT, FssLanguageInfo.LanguageNo languageNo)
        {
            var langageInfoJp = new FssLanguageInfo();
            langageInfoJp.SetFile(new FssLanguageFile());

            langageInfoJp.SetHeader(langageInfoEn.LanguageHeader.Clone());

            foreach (var langEntryEn in langageInfoEn.LanguageFile.Items.Values)
            {
                var langEntryJp = langEntryEn.Clone();

                string translatedText = sheetFile.Translate(
                    langEntryEn.EntryID, langEntryEn.Texts[(int)languageNo], useMT);
                langEntryJp.Texts[(int)languageNo] = translatedText;

                langageInfoJp.LanguageFile.AddEntry(langEntryJp);
            }

            langageInfoJp.SetFooter(langageInfoEn.LanguageFooter.Clone());

            return langageInfoJp;
        }
    }
}
