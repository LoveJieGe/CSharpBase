using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Chapter31_ADONETDemo
{
    public class ExcelUtils
    {
        private static object Nothing = Missing.Value;//由于COM组件很多值需要用Missing.Value代替   
        
       
        public static void Export(string filepath,string filepath2)
        {
            // 初始化
            //根据路径path打开
            Excel.Application excelApp = new Excel.ApplicationClass()
            {
                // 设置是否显示警告窗体
                DisplayAlerts = false,
                //设置是否显示Excel
                Visible = false,
                //禁止刷新屏幕
                ScreenUpdating = false
            };
            Excel.Workbook workbook = null;
            Excel.Worksheet sheet= null;
            try
            {
                workbook = GetWorkBook(excelApp,filepath);
                sheet = NewSheet(workbook);
                ExcelXmlHelper.FillSheet(filepath2, sheet);
                Object format = Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal;//获取Excl 2007文件格式   
                workbook.SaveAs(filepath, Nothing, Nothing, Nothing, Nothing, Nothing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Nothing, Nothing, Nothing, Nothing, Nothing);//保存为Excl 2007格式   
            }
            catch (Exception e)
            {

            }
            finally
            {
                CloseExcel(excelApp, workbook);
            }
        }
        public static Excel.Workbook GetWorkBook(Excel.Application excelApp,string filepath)
        {
            if (!File.Exists(filepath))
                throw new Exception("找不到文件");
            
            Excel.Workbook xlsWorkBook = excelApp.Workbooks.Open(filepath, Nothing, Nothing, Nothing, Nothing, Nothing,
                Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);
            return xlsWorkBook;
        }

        public static Excel.Worksheet NewSheet(Excel.Workbook xlsWorkBook)
        {
            Excel.Sheets sheets = xlsWorkBook.Worksheets;
            
            Excel.Worksheet workSheet = sheets.Add(Nothing, sheets[sheets.Count]) as Excel.Worksheet;
            return workSheet;
        }

        //关闭打开的Excel方法
        public static void CloseExcel(Excel.Application excelApp, Excel.Workbook excelWorkbook)
        {
            excelWorkbook.Close(false, Type.Missing, Type.Missing);
            excelWorkbook = null;
            excelApp.Quit();
            GC.Collect();
            ExcelProcess.Kill(excelApp);
        }
    }
    /// <summary>
    /// 关闭Excel进程
    /// </summary>
    public class ExcelProcess
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public static void Kill(Excel.Application excel)
        {
            try
            {
                IntPtr t = new IntPtr(excel.Hwnd);   //得到这个句柄，具体作用是得到这块内存入口
                int k = 0;
                GetWindowThreadProcessId(t, out k);   //得到本进程唯一标志k
                System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);   //得到对进程k的引用
                p.Kill();     //关闭进程k
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
