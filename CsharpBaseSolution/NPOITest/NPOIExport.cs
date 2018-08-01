using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.OpenXml4Net.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace NPOITest
{
    public class NPOIExport
    {
        public static HSSFWorkbook NPOIOpenHSSFExcel(string filename)
        {
            HSSFWorkbook myHSSFWorkbook;
            Stream myExcelStream = OpenClasspathResource(filename);
            myHSSFWorkbook = new HSSFWorkbook(myExcelStream);
            return myHSSFWorkbook;
        }
        public static XSSFWorkbook NPOIOpenXSSFExcel(string filename)
        {
            XSSFWorkbook workbook;
            Stream myExcelStream = OpenClasspathResource(filename);
            workbook = new XSSFWorkbook(myExcelStream);
            return workbook;
        }
        private static  Stream OpenClasspathResource(String fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return file;
        }
        public static void AppendSheet(HSSFWorkbook workbook, ISheet sheet, string sheetname = "Sheet0")
        {
            sheet = workbook.CreateSheet(sheetname);//创建一个名称为Sheet0的表
            workbook.Add(sheet);
        }
        public static void AddData(DataTable dt, ISheet sheet)
        {
            int rowCount = dt.Rows.Count;//行数  
            int columnCount = dt.Columns.Count;//列数  
                                               //设置列头  
            IRow row = null;
            ICell cell = null;
            try
            {
                
                row = sheet.CreateRow(0);//excel第一行设为列头  
                for (int c = 0; c < columnCount; c++)
                {
                    cell = row.CreateCell(c);
                    ICellStyle style = cell.CellStyle;
                    cell.SetCellValue(dt.Columns[c].ColumnName);
                }
            }
            catch 
            { }
           
        }
        public static XmlDocument ReadFile(string filename)
        {
            XmlDocument doc = new XmlDocument();
            HSSFWorkbook workbook = new HSSFWorkbook();
            XSSFWorkbook workbook2;
            doc.Load(filename);
            try
            {
                workbook = NPOIOpenHSSFExcel(filename);
            }
            catch (Exception e)
            {
                workbook2 = NPOIOpenXSSFExcel(filename);
            }
            //Stream myExcelStream = OpenClasspathResource(filename);
            //XmlHelper.LoadXmlSafe(doc, myExcelStream);
            XmlNodeList nodelist = doc.GetElementsByTagName("Style");
            foreach (XmlNode node in nodelist)
            {
                NPOIStyle style = new NPOIStyle();
                ICellStyle cellStyle = workbook.CreateCellStyle();
                XmlNode styleIDNode = node.Attributes.GetNamedItem("ss:ID");
                style.StyleID = styleIDNode.Value;
                XmlNodeList childList = node.ChildNodes;
                foreach (XmlNode child in childList)
                {
                    if (string.Equals(child.Name, "Font", StringComparison.CurrentCultureIgnoreCase))
                    {
                        XmlNode fontNameNode = child.Attributes.GetNamedItem("ss:FontName");
                        XmlNode fontSize = child.Attributes.GetNamedItem("ss:Size");
                        XmlNode fontBold = child.Attributes.GetNamedItem("ss:Bold");
                        XmlNode fontcolor = child.Attributes.GetNamedItem("ss:Color");
                        IFont font = workbook.CreateFont();
                        if (fontNameNode != null)
                            font.FontName = fontNameNode.Value;
                        if (fontBold != null)
                            font.Boldweight = Convert.ToInt16(fontBold.Value);
                        if (fontSize != null)
                            font.FontHeightInPoints = Convert.ToInt16(fontSize.Value);
                        if (fontcolor != null)
                        {
                            if (string.Equals(fontcolor.Value, "Automatic", StringComparison.CurrentCultureIgnoreCase))
                            {
                                font.Color = HSSFColor.Automatic.Index;
                            }
                            else
                            {
                            }
                        }
                        cellStyle.SetFont(font);
                    }
                    if (string.Equals(child.Name, "NumberFormat", StringComparison.CurrentCultureIgnoreCase))
                    {
                        XmlNode formatNode = child.Attributes.GetNamedItem("ss:Format");
                        if (formatNode != null)
                            cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat(formatNode.Value);
                    }
                    if (string.Equals(child.Name, "Interior", StringComparison.CurrentCultureIgnoreCase))
                    {
                        XmlNode formatNode = child.Attributes.GetNamedItem("ss:Format");
                        if (formatNode != null)
                            cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat(formatNode.Value);
                    }
                }
                style.CellStyle = cellStyle;
            }
            return doc;
        }
    }
}
