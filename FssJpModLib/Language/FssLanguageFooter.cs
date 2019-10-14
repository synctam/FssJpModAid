namespace FssJpModLib.Language
{
    using System;
    using System.IO;
    using FsbCommonLib;

    /// <summary>
    /// 言語フッター
    /// </summary>
    public class FssLanguageFooter
    {
        /// <summary>
        /// 言語テーブルファイル
        /// </summary>
        public FsbLanguageTableFile LanguageCategoryFile { get; private set; } = new FsbLanguageTableFile();

        public bool CaseInsensitiveTerms { get; private set; }

        public int AssetsCount { get; private set; }

        public bool NeverDestroy { get; private set; }

        public bool UserAgreesToHaveItOnTheScene { get; private set; }

        /// <summary>
        /// Streamからデータを読み込み、言語フッターを返す。
        /// </summary>
        /// <param name="reader">Stream</param>
        /// <returns>言語フッター</returns>
        public static FssLanguageFooter Read(BinaryReader reader)
        {
            var fssLanguageFooter = new FssLanguageFooter();
            try
            {
                fssLanguageFooter.LanguageCategoryFile.Read(reader);

                fssLanguageFooter.CaseInsensitiveTerms = FsbBinUtils.ReadBoolean(reader);
                fssLanguageFooter.AssetsCount = reader.ReadInt32();
                //// ToDo: Assets配列の処理を追加する。

                fssLanguageFooter.NeverDestroy = FsbBinUtils.ReadBoolean(reader);
                fssLanguageFooter.UserAgreesToHaveItOnTheScene = FsbBinUtils.ReadBoolean(reader);
            }
            catch
            {
                throw;
            }

            return fssLanguageFooter;
        }

        /// <summary>
        /// Streamにデータを書き込む。
        /// </summary>
        /// <param name="bw">Stream</param>
        public void Write(BinaryWriter bw)
        {
            this.LanguageCategoryFile.Write(bw);

            FsbBinUtils.WriteBoolean(bw, this.CaseInsensitiveTerms);
            bw.Write(this.AssetsCount);
            FsbBinUtils.WriteBoolean(bw, this.NeverDestroy);
            FsbBinUtils.WriteBoolean(bw, this.UserAgreesToHaveItOnTheScene);
        }

        /// <summary>
        /// 自分自身のクローンを返す。
        /// </summary>
        /// <returns>自分自身のクローン</returns>
        public FssLanguageFooter Clone()
        {
            var languageFooter = new FssLanguageFooter()
            {
                LanguageCategoryFile = this.LanguageCategoryFile.Clone(),
                CaseInsensitiveTerms = this.CaseInsensitiveTerms,
                AssetsCount = this.AssetsCount,
                NeverDestroy = this.NeverDestroy,
                UserAgreesToHaveItOnTheScene = this.UserAgreesToHaveItOnTheScene,
            };

            return languageFooter;
        }
    }
}
