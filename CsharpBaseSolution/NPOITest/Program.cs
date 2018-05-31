using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPOITest
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\songtaojie\Downloads\总分类账_20180522_160534.xls";
            NPOIExport.ReadFile(path);
            Console.ReadLine();
        }
    }
}
