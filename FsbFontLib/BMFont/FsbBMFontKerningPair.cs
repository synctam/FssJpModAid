namespace FsbFontLib.BMFont
{
    /// <summary>
    /// カーニングペア
    /// </summary>
    public class FsbBMFontKerningPair
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="first">一文字目の文字コード</param>
        /// <param name="second">二文字目の文字コード</param>
        /// <param name="amount">文字間隔</param>
        public FsbBMFontKerningPair(int first, int second, int amount)
        {
            this.First = first;
            this.Second = second;
            this.Amount = amount;
        }

        /// <summary>
        /// 一文字目の文字コード
        /// </summary>
        public int First { get; } = -1;

        /// <summary>
        /// 二文字目の文字コード
        /// </summary>
        public int Second { get; } = -1;

        /// <summary>
        /// 文字間隔
        /// </summary>
        public int Amount { get; } = 0;
    }
}