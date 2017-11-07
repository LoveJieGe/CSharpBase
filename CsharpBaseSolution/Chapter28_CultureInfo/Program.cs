using System;
using System.Globalization;
using System.Threading;

namespace Chapter28_CultureInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberFormat(1234567890);
            Console.ReadKey();
        }
        private static void NumberFormat(int value)
        {
            //使用本地的
            Console.WriteLine(value.ToString("N"));
            //使用特定的
            Console.WriteLine(value.ToString("N", new CultureInfo("fr-FR")));
            //使用现线程
            Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE");
            Console.WriteLine(value.ToString("N"));

        }
    }
}
