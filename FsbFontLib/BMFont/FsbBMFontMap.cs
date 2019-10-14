namespace FsbFontLib.BMFont
{
    using System;
    using System.Collections.Generic;

    public class FsbBMFontMap
    {
        /// <summary>
        /// フォントマップの辞書。
        /// キーは文字コード。
        /// </summary>
        public IDictionary<int, FsbBMFontCharacter> Items { get; } =
            new SortedDictionary<int, FsbBMFontCharacter>();

        /// <summary>
        /// フォントサイズ
        /// </summary>
        public int FontSize { get; set; } = 0;

        /// <summary>
        /// フォントベース
        /// </summary>
        public int FontBase { get; set; } = 0;

        /// <summary>
        /// イメージの幅
        /// </summary>
        public int ImageWidth { get; set; } = 0;

        /// <summary>
        /// イメージの高さ
        /// </summary>
        public int ImageHeight { get; set; } = 0;

        /// <summary>
        /// 文字エントリーを追加する。
        /// </summary>
        /// <param name="entry">文字エントリー</param>
        public void AddEntry(FsbBMFontCharacter entry)
        {
            if (this.Items.ContainsKey(entry.CharacterID))
            {
                var msg = $"Duplicate character-id({entry.CharacterID}).";
                throw new Exception(msg);
            }
            else
            {
                this.Items.Add(entry.CharacterID, entry);
            }
        }

        /// <summary>
        /// カーニングペアを追加する。
        /// </summary>
        /// <param name="kerningPair">カーニングペア</param>
        public void AddKerningPair(FsbBMFontKerningPair kerningPair)
        {
            if (this.Items.ContainsKey(kerningPair.First))
            {
                var entry = this.Items[kerningPair.First];
                entry.Kernings.Add(kerningPair);
            }
            else
            {
                throw new Exception(
                    $"Character not found. CharacterID({kerningPair.First})");
            }
        }
    }
}
