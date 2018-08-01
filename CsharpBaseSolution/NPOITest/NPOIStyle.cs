using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPOITest
{
    public class NPOIStyle
    {
        public string StyleID
        {
            get;set;
        }
        public ICellStyle CellStyle
        {
            get;set;
        }
    }
}
