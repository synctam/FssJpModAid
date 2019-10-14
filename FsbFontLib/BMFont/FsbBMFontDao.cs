namespace FsbFontLib.BMFont
{
    using System.IO;
    using System.Text;
    using System.Xml;

    public class FsbBMFontDao
    {
        /// <summary>
        /// BMFontで作成されたXML形式の座標情報を読み込み、
        /// フォントマップ オブジェクを返す。
        /// </summary>
        /// <param name="path">座標情報ファイルのパス</param>
        /// <returns>フォントマップ オブジェク</returns>
        public static FsbBMFontMap Load(string path)
        {
            var xml = new XmlDocument();

            using (var fs = new FileStream(
                path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    var text = sr.ReadToEnd();
                    xml.LoadXml(text);
                }
            }

            XmlNode info = xml.GetElementsByTagName("info")[0];
            XmlNode common = xml.GetElementsByTagName("common")[0];

            var fontMap = new FsbBMFontMap();
            fontMap.FontSize = int.Parse(GetValue(info, "size"));
            fontMap.ImageWidth = int.Parse(GetValue(common, "scaleW"));
            fontMap.ImageHeight = int.Parse(GetValue(common, "scaleH"));
            fontMap.FontBase = int.Parse(GetValue(common, "base"));

            //// キャラクターの情報を処理する
            {
                XmlNodeList chars = xml.GetElementsByTagName("chars")[0].ChildNodes;
                for (int i = 0; i < chars.Count; i++)
                {
                    XmlNode charNode = chars[i];
                    if (charNode.Attributes != null)
                    {
                        var entry = new FsbBMFontCharacter();
                        entry.CharacterID = int.Parse(GetValue(charNode, "id"));
                        entry.PosX = int.Parse(GetValue(charNode, "x"));
                        entry.PosY = int.Parse(GetValue(charNode, "y"));
                        entry.Width = int.Parse(GetValue(charNode, "width"));
                        entry.Height = int.Parse(GetValue(charNode, "height"));
                        entry.OffsetX = int.Parse(GetValue(charNode, "xoffset"));
                        entry.OffsetY = int.Parse(GetValue(charNode, "yoffset"));

                        entry.AdvanceX = int.Parse(GetValue(charNode, "xadvance"));
                        entry.Channel = int.Parse(GetValue(charNode, "chnl"));

                        fontMap.AddEntry(entry);
                    }
                }
            }

            //// カーニングの情報を処理する。
            {
                XmlNodeList kernings = xml.GetElementsByTagName("kernings")[0].ChildNodes;
                for (int i = 0; i < kernings.Count; i++)
                {
                    XmlNode kerningNode = kernings[i];
                    if (kerningNode.Attributes != null)
                    {
                        var first = int.Parse(GetValue(kerningNode, "first"));
                        var second = int.Parse(GetValue(kerningNode, "second"));
                        var amount = int.Parse(GetValue(kerningNode, "amount"));

                        var entry = new FsbBMFontKerningPair(first, second, amount);
                        fontMap.AddKerningPair(entry);
                    }
                }
            }

            return fontMap;
        }

        /// <summary>
        /// 指定されたノードからnameの値を返す。
        /// </summary>
        /// <param name="node">ノード</param>
        /// <param name="name">名称</param>
        /// <returns>値</returns>
        private static string GetValue(XmlNode node, string name)
        {
            return node.Attributes.GetNamedItem(name).InnerText;
        }
    }
}
