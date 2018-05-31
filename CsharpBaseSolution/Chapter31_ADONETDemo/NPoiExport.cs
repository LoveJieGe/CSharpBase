using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter31_ADONETDemo
{
    public class NPoiExport
    {
        public HSSFWorkbook NPOIOpenExcel(string filename)
        {
            HSSFWorkbook myHSSFWorkbook;
            Stream myExcelStream = OpenClasspathResource(filename);
            myHSSFWorkbook = new HSSFWorkbook(myExcelStream);
            return myHSSFWorkbook;
        }
        private Stream OpenClasspathResource(String fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return file;
        }
    }
}
