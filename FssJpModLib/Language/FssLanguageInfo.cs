namespace FssJpModLib.Language
{
    /// <summary>
    /// 言語情報
    /// </summary>
    public class FssLanguageInfo
    {
        /// <summary>
        /// 言語番号
        /// </summary>
        public enum LanguageNo
        {
            English = 0,
            Spanish = 1,
            French = 2,
            Italian = 3,
            German = 4,
            Russian = 5,
        }

        /// <summary>
        /// 言語ヘッダー
        /// </summary>
        public FssLanguageHeader LanguageHeader { get; private set; } = null;

        /// <summary>
        /// 言語ファイル
        /// </summary>
        public FssLanguageFile LanguageFile { get; private set; } = null;

        /// <summary>
        /// 言語フッター
        /// </summary>
        public FssLanguageFooter LanguageFooter { get; private set; } = null;

        /// <summary>
        /// 言語ヘッダーを設定する。
        /// </summary>
        /// <param name="fssLanguageHeader">言語ヘッダー</param>
        public void SetHeader(FssLanguageHeader fssLanguageHeader)
        {
            this.LanguageHeader = fssLanguageHeader;
        }

        /// <summary>
        /// 言語ファイルを設定する。
        /// </summary>
        /// <param name="fssLanguageFile">言語ファイル</param>
        public void SetFile(FssLanguageFile fssLanguageFile)
        {
            this.LanguageFile = fssLanguageFile;
        }

        /// <summary>
        /// 言語フッターの設定
        /// </summary>
        /// <param name="fssLanguageFooter">言語フッター</param>
        public void SetFooter(FssLanguageFooter fssLanguageFooter)
        {
            this.LanguageFooter = fssLanguageFooter;
        }
    }
}
