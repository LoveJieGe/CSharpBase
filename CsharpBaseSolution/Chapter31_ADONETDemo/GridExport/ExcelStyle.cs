using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter31_ADONETDemo
{
    public class ExcelStyle
    {
        public string StyleID
        {
            get;set;
        }
        public int FontBlod
        {
            get;set;
        }
        public string FontName
        {
            get;set;
        }
        public int FontSize
        {
            get;set;
        }
        public string FontColor
        {
            get;set;
        }
        public string InteriorColor
        {
            get;set;
        }
        public string InteriorPattern
        {
            get; set;
        }
        public string NumberFormat
        {
            get;set;
        }
        public string AlignmentVertical
        {
            get;set; 
        }
        public string AlignmentHorizontal
        {
            get; set;
        }
        public string AlignmentWrapText
        {
            get;set;
        }
    }
}
