using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Chapter31_ADONETDemo
{
    public class Export
    {
        public static XmlDocument ExcelToXml(string filePath)
        {
            XmlDocument excelData = new XmlDocument();
            //excelData.Load(filePath);
            Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
            //workbook.Worksheets.Add("明细账");
            workbook.LoadFromFile(filePath);
            string tempPath = Path.GetTempFileName();
            workbook.SaveAsXml(tempPath);
            excelData.Load(tempPath);
            File.Delete(tempPath);
            return excelData;
        }
        public static XmlDocument LoadXml(string filePath)
        {
            XmlDocument excelData = new XmlDocument();
            //TextReader reader = new StringReader(filePath);
            excelData.Load(filePath);
            XmlNodeList sheetList = excelData.GetElementsByTagName("Worksheet");
            XmlNodeList titleList = excelData.GetElementsByTagName("Style");
            return excelData;
        }

        public static XmlDocument ConvertExcel(string savePath)
        {
            //将xml文件转换为标准的Excel格式 
            XmlDocument excelData = new XmlDocument();
            Object Nothing = Missing.Value;//由于yongCOM组件很多值需要用Missing.Value代替   
            Application ExclApp = new Microsoft.Office.Interop.Excel.ApplicationClass();// 初始化
            Microsoft.Office.Interop.Excel.Workbook ExclDoc = ExclApp.Workbooks.Open(savePath, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);//打开Excl工作薄   
            try
            {
                Sheets sheets = ExclDoc.Worksheets;
                Styles styles = ExclDoc.Styles;
                Style s1 = styles[1];
                Font f1 = s1.Font;
                object n1 = f1.Name;
                Interior i1 = s1.Interior;
                string name1 = s1.Name;
                Style s2 = styles[2];
                Font f2 = s2.Font;
                object n2 = f2.Name;
                Interior i2 = s2.Interior;
                string name2 = s2.Name;
                Style s3 = styles[3];
                Style s4 = styles[4];
                Microsoft.Office.Interop.Excel.Worksheet sheet = sheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
                var row = sheet.Rows;
                var col = sheet.Columns;
                string name = sheet.Name;

                PivotTables tables = sheet.PivotTables() as PivotTables;
                int tableId = tables.Count;
                Range cell = sheet.Cells;
                int c1 = row.Count;
                int c2 = col.Count;

                Range range = sheet.UsedRange;
                int rc = range.Count;
                Range r1 = range[1, 1] as Range;
                object names = r1.Font.Name;
                object m = r1.MergeCells;
                object size = r1.Font.Size;
                object b = r1.Font.Bold;
                Interior inte = r1.Interior;
                object c = inte.Color;
                object p = inte.Pattern;
                object text = r1.Text;
                object ss = r1.Style;
                object w = r1.Width;
                object wtext = r1.WrapText;
                Object format = Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal;//获取Excl 2007文件格式   
                ExclApp.DisplayAlerts = false;
                ExclDoc.SaveAs(savePath, format, Nothing, Nothing, Nothing, Nothing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Nothing, Nothing, Nothing, Nothing, Nothing);//保存为Excl 2007格式   
            }
            catch (Exception ex) { }
            ExclDoc.Close(Nothing, Nothing, Nothing);
            ExclApp.Quit();
            return excelData;
        }
        public static void GetImport()
        {
            var type = typeof(XmlMap);
            var types = AppDomain.CurrentDomain.GetAssemblies()
.SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(XmlMap))))
.ToArray();
            foreach (var v in types)
            {
                if (v.IsClass)
                {
                    Console.WriteLine(v.Name+":"+v.Namespace);
                }
            }
        }
    }
    //public class XmlMapTest : XmlMap
    //{

    //}
}
