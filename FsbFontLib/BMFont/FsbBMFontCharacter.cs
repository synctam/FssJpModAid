namespace FsbFontLib.BMFont
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FsbBMFontCharacter
    {
        public int CharacterID { get; set; }

        public int PosX { get; set; }

        public int PosY { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int OffsetX { get; set; }

        public int OffsetY { get; set; }

        public int AdvanceX { get; set; }

        public int Channel { get; set; }

        /// <summary>
        /// カーニングペア
        /// </summary>
        public List<FsbBMFontKerningPair> Kernings { get; } = new List<FsbBMFontKerningPair>();
    }
}
