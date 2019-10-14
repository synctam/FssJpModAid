namespace FsbFontLib.Convert
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FsbFontLib.BMFont;
    using FsbFontLib.Font;

    public class FsbFontConvert
    {
        /// <summary>
        /// 座標情報(jp)に座標情報(en)をマージする。
        /// </summary>
        /// <param name="en">座標情報(英語)</param>
        /// <param name="jp">座標情報(日本語)</param>
        /// <param name="replacedList">置換されたCharacterIDを格納</param>
        /// <param name="offsetX">オフセットX</param>
        /// <param name="offsetY">オフセットY</param>
        /// <returns>マージ後の座標情報</returns>
        public static FsbFont Merge(
            FsbFont en,
            FsbBMFontMap jp,
            StringBuilder replacedList,
            int offsetX,
            int offsetY)
        {
            var newFont = new FsbFont();

            //// オリジナルのHeaderをセットする。
            newFont.Header.SetHeader(en.Header);

            if (jp != null)
            {
                //// Headerの情報の一部を日本語のものに変換する。
                newFont.Header.Convert(jp);
                //// 座標情報(日本語版)をセットする。
                var rc = newFont.FontEntries.UpsertEntries(jp.Items, offsetX, offsetY);
                foreach (var characterID in rc)
                {
                    //// 登録できなかったCharacterIDを記録する。
                    replacedList.AppendLine($"{characterID}");
                }
            }

            //// オリジナルのFooterをセットする。
            newFont.Footer.SetFooter(en.Footer);

            return newFont;
        }
    }
}
