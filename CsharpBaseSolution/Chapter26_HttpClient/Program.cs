using System;
using System.Diagnostics;

namespace Chapter26_HttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("调用方法之前");
            //HttpClientExample.GetData();
            //Console.WriteLine("调用方法之后");
            //Console.ReadKey();

            Process process = new Process();
            process.StartInfo.FileName = "iexplore.exe";
            process.StartInfo.Arguments = "http://www.baidu.com";
            process.Start();
        }
    }
}
