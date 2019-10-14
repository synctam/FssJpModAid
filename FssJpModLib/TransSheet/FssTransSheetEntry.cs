namespace FssJpModLib.TransSheet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FssTransSheetEntry
    {
        public string EntryID { get; set; }

        public string Hint { get; set; }

        public string LangEN { get; set; }

        public string LangJP { get; set; }

        public string LangMT { get; set; }

        public int Sequence { get; set; }
    }
}
