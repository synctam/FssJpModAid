namespace FsbFontLib.Font
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using FsbFontLib.BMFont;

    /// <summary>
    /// フォント・エントリーのコレクション
    /// </summary>
    public class FsbFontEntries
    {
        /// <summary>
        /// フォント・エントリの辞書
        /// キーは CharacterID。
        /// </summary>
        public IDictionary<int, FsbFontEntry> Items { get; } =
            new SortedDictionary<int, FsbFontEntry>();

        /// <summary>
        /// Streamからフォント・エントリーを読み込む。
        /// </summary>
        /// <param name="reader">Stream</param>
        public void Read(BinaryReader reader)
        {
            var entryCount = reader.ReadInt32();
            for (int i = 0; i < entryCount; i++)
            {
                var entry = new FsbFontEntry();
                entry.Read(reader);
                this.Items.Add(entry.CharacterID, entry);
            }
        }

        /// <summary>
        /// フォント・エントリーに値を設定する。
        /// </summary>
        /// <param name="data">フォント・エントリーのコレクション</param>
        public void SetEntries(FsbFontEntries data)
        {
            foreach (var newEntry in data.Items.Values)
            {
                if (this.Items.ContainsKey(newEntry.CharacterID))
                {
                    //// CharacterIDが登録済みの場合は更新する。
                    var oldEntry = this.Items[newEntry.CharacterID];
                    oldEntry.CharacterID = newEntry.CharacterID;
                    oldEntry.PosX = newEntry.PosX;
                    oldEntry.PosY = newEntry.PosY;
                    oldEntry.Width = newEntry.Width;
                    oldEntry.Height = newEntry.Height;
                    oldEntry.OffsetX = newEntry.OffsetX;
                    oldEntry.OffsetY = newEntry.OffsetY;
                    oldEntry.AdvanceX = newEntry.AdvanceX;
                    oldEntry.Channel = newEntry.Channel;
                    oldEntry.Kernings.Clear();
                    foreach (var newKernPair in newEntry.Kernings)
                    {
                        oldEntry.Kernings.Add(newKernPair);
                    }
                }
                else
                {
                    //// 未登録の場合は追加する。
                    this.Items.Add(newEntry.CharacterID, newEntry);
                }
            }
        }

        /// <summary>
        /// 指定されたコレクションに含まれるエントリーを設定する。
        /// </summary>
        /// <param name="data">コレクション</param>
        public void SetEntries(IDictionary<int, FsbFontEntry> data)
        {
            this.Items.Clear();

            foreach (var newEntry in data.Values)
            {
                this.Items.Add(newEntry.CharacterID, newEntry);
            }
        }

        /// <summary>
        /// 指定したコレクションに含まれるエントリーに全て置き換える。
        /// </summary>
        /// <param name="data">コレクション</param>
        /// <param name="offsetX">X座標のオフセット</param>
        /// <param name="offsetY">Y座標のオフセット</param>
        /// <returns>登録できなかったCharacterIDのリスト</returns>
        public List<int> UpsertEntries(
            IDictionary<int, FsbBMFontCharacter> data, int offsetX, int offsetY)
        {
            var result = new List<int>();

            foreach (var bmFontEntry in data.Values)
            {
                var newEntry = new FsbFontEntry();
                newEntry.CharacterID = bmFontEntry.CharacterID;
                newEntry.PosX = bmFontEntry.PosX;
                newEntry.PosY = bmFontEntry.PosY;
                newEntry.Width = bmFontEntry.Width;
                newEntry.Height = bmFontEntry.Height;
                newEntry.OffsetX = bmFontEntry.OffsetX;
                newEntry.OffsetY = bmFontEntry.OffsetY;
                newEntry.AdvanceX = bmFontEntry.AdvanceX;
                newEntry.Channel = bmFontEntry.Channel;
                foreach (var kerningPair in bmFontEntry.Kernings)
                {
                    var newPair = new FsbFontKerningPair();

                    newPair.CharacterID = kerningPair.Second;
                    newPair.Amount = kerningPair.Amount;

                    newEntry.Kernings.Add(newPair);
                }

                newEntry.PosX = newEntry.PosX + offsetX;
                newEntry.PosY = newEntry.PosY + offsetY;

                var rc = this.AddEntry(newEntry);
                if (!rc)
                {
                    //// 登録できなかったCharacterIDを記録する.
                    result.Add(newEntry.CharacterID);
                }
            }

            return result;
        }

        /// <summary>
        /// Writerで指定したストリームにデータを書き出す。
        /// </summary>
        /// <param name="writer">Writer</param>
        public void Write(BinaryWriter writer)
        {
            writer.Write(this.Items.Count);
            foreach (var entry in this.Items.Values)
            {
                entry.Write(writer);
            }
        }

        /// <summary>
        /// 指定したエントリーを追加する。
        /// </summary>
        /// <param name="entry">エントリー</param>
        /// <returns>追加の成否。True: 追加に成功。</returns>
        public bool AddEntry(FsbFontEntry entry)
        {
            if (this.Items.ContainsKey(entry.CharacterID))
            {
                //// Duplicate character({entry.CharacterID}).
                return false;
            }
            else
            {
                this.Items.Add(entry.CharacterID, entry);

                return true;
            }
        }

        public FsbFontEntries Clone()
        {
            var fsbFontEntries = new FsbFontEntries();

            foreach (var entry in this.Items.Values)
            {
                var newEntry = entry.Clone();
                fsbFontEntries.AddEntry(newEntry);
            }

            return fsbFontEntries;
        }

        /// <summary>
        /// デバッグ用テキストを返す。
        /// </summary>
        /// <returns>デバッグ用テキスト</returns>
        public override string ToString()
        {
            var buff = new StringBuilder();

            buff.AppendLine($"Count({this.Items.Count})");
            foreach (var entry in this.Items.Values)
            {
                buff.Append(entry.ToString());
            }

            return buff.ToString();
        }
    }
}
