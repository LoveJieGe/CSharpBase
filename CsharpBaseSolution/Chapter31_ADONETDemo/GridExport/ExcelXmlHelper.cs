using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Chapter31_ADONETDemo
{
    public class ExcelXmlHelper
    {
        public static XmlDocument ReadFileByName(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            return doc;
        }
        public static XmlDocument ReadFileByXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }
        public static Dictionary<string,ExcelStyle> FillStyle(XmlDocument doc)
        {
            Dictionary<string, ExcelStyle> styleList = new Dictionary<string, ExcelStyle>();
            XmlNodeList nodelist = doc.GetElementsByTagName("Style");
            foreach (XmlNode node in nodelist)
            {
                ExcelStyle style = new ExcelStyle();
                XmlNode styleIDNode = node.Attributes.GetNamedItem("ss:ID");
                style.StyleID = styleIDNode.Value;
                XmlNodeList childList = node.ChildNodes;
                foreach (XmlNode child in childList)
                {
                    switch (child.Name.ToLower())
                    {
                        case "font":
                            XmlNode fontNameNode = child.Attributes.GetNamedItem("ss:FontName");
                            XmlNode fontSize = child.Attributes.GetNamedItem("ss:Size");
                            XmlNode fontBold = child.Attributes.GetNamedItem("ss:Bold");
                            XmlNode fontcolor = child.Attributes.GetNamedItem("ss:Color");
                            if (fontNameNode != null)
                                style.FontName = fontNameNode.Value;
                            if (fontBold != null)
                                style.FontBlod = Convert.ToInt16(fontBold.Value);
                            if (fontSize != null)
                                style.FontSize = Convert.ToInt16(fontSize.Value);
                            if (fontcolor != null)
                            {
                                style.FontColor = fontcolor.Value;
                            }
                            break;
                        case "numberformat":
                            XmlNode formatNode = child.Attributes.GetNamedItem("ss:Format");
                            if (formatNode != null)
                                style.NumberFormat = formatNode.Value;
                            break;
                        case "interior":
                            XmlNode colorNode = child.Attributes.GetNamedItem("ss:Color");
                            XmlNode patternNode = child.Attributes.GetNamedItem("ss:Pattern");
                            if (colorNode != null)
                                style.InteriorColor = colorNode.Value;
                            if (patternNode != null)
                                style.InteriorPattern = patternNode.Value;
                            break;
                        case "alignment":
                            XmlNode verticalNode = child.Attributes.GetNamedItem("ss:Vertical");
                            XmlNode horizontalNode = child.Attributes.GetNamedItem("ss:Horizontal");
                            XmlNode wrapNode = child.Attributes.GetNamedItem("ss:WrapText");
                            if (horizontalNode != null)
                                style.AlignmentHorizontal = horizontalNode.Value;
                            if (verticalNode != null)
                                style.AlignmentVertical = verticalNode.Value;
                            if (wrapNode != null)
                                style.AlignmentWrapText = wrapNode.Value;
                            break;
                    }
                }
                if(!styleList.ContainsKey(styleIDNode.Value))
                    styleList.Add(styleIDNode.Value,style);
            }
            return styleList;
        }

        public static DataSet FillData(XmlDocument doc)
        {
            DataSet set = new DataSet();
            XmlNodeList nodeList = doc.GetElementsByTagName("Worksheet");
            XmlNode node = nodeList.Item(0);
            if (node != null)
            {
                StringReader stream = new StringReader(node.OuterXml);
                XmlTextReader reader = new XmlTextReader(stream);
                set.ReadXml(reader);
            }
            return set;
        }
        public static void FillSheet(string filepath,Excel.Worksheet sheet)
        {
            XmlDocument doc = ReadFileByName(filepath);
            Dictionary<string, ExcelStyle>  styleList = FillStyle(doc);
            DataSet set = FillData(doc);
            //工作簿名称
            DataTable sheetTable = set.Tables["Worksheet"];
            if (sheetTable != null && sheetTable.Rows.Count>0)
            {
                sheet.Name = sheetTable.Rows[0]["Name"].ToString()+"_"+DateTime.Now.Millisecond;
            }
            DataTable rowTable = set.Tables["Row"];
            DataTable cellTable = set.Tables["Cell"];
            DataTable dataTable = set.Tables["Data"];
            int colIndex = 0;
            foreach (DataRow row in rowTable.Rows)
            {
                DataRow[] cellRows = cellTable.Select("Row_Id=" + row["Row_Id"]);
                foreach (DataRow cellRow in cellRows)
                {
                    colIndex++;
                    string merge = cellRow["MergeAcross"].ToString();
                    if (!string.IsNullOrEmpty(merge))
                    {
                        colIndex+=Convert.ToInt16(merge);

                    }
                    
                    DataRow[] dataRows = dataTable.Select("Row_Id=" + row["Cell_Id"]);
                    if (dataRows != null && dataRows.Length > 0)
                    {
                        DataRow dataRow = dataRows[0];
                    }
                }
            }
            //if (set != null && set.Tables.Count > 0)
            //{
            //    DataTable columnTable = set.Tables["Column"];
            //    int colIndex = 0;
            //    foreach (DataRow row in columnTable.Rows)
            //    {
            //        colIndex++;
            //        int count = sheet.Columns.Count;
            //        var range = (Excel.Range)sheet.Columns[colIndex];
            //        range.ColumnWidth = row["Width"];
            //        range.AutoFit();
            //        if (row["StyleID"] != null && !string.IsNullOrEmpty(row["StyleID"].ToString()))
            //        {
            //            ExcelStyle style = null;
            //            string styleID = row["StyleID"].ToString();
            //            styleList.TryGetValue(styleID, out style);
            //            if (style != null)
            //            {
            //                range.Font.Color = style.FontColor;
            //                range.Font.Size = style.FontSize;
            //                range.Font.Name = style.FontName;
            //                range.Font.Bold = style.FontBlod;
            //                range.Interior.Color = style.InteriorColor;
            //                range.NumberFormat = style.NumberFormat;
            //                range.VerticalAlignment = style.AlignmentVertical;
            //                range.WrapText = style.AlignmentWrapText;
            //                range.HorizontalAlignment = style.AlignmentHorizontal;
            //            }
            //        }
            //    }

            //写入标题    
            //foreach (System.Data.DataColumn col in dt.Columns)
            //{
            //    colIndex++;
            //    ////appExcel.Cells[1, colIndex] = col.ColumnName;  
            //    //worksheetData.Cells[1, colIndex] = col.ColumnName;
            //    //range = (Excel.Range)sheet.Cells[1, colIndex];
            //    //range.RowHeight = 25;            //行高  
            //    //                                 //range.EntireRow.AutoFit();      //自动调整行高  
            //    //range.Interior.ColorIndex = 15;     //设置颜色  
            //    //                                    //range.Font.Size = 10;       //字体大小  
            //    //range.Font.Bold = true;       //加粗  
            //    //range.NumberFormatLocal = "@";//文本格式    
            //    //range.EntireColumn.AutoFit();//自动调整列宽    
            //    //                             // range.WrapText = true; //文本自动换行      
            //    //                             //range.ColumnWidth = 25;        //列宽  
            //    //range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;    //居中  
            //}
        //}
           
        }
    }
}
