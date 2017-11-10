using System;
using System.Globalization;
using System.Threading;

namespace Chapter28_CultureInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            //NumberFormat(1234567890);
            DateTimeFormat();
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
        private static void DateTimeFormat()
        {
            DateTime d = new DateTime(2017, 11, 8);
            Console.WriteLine(d.ToLongDateString());
            Console.WriteLine("-----------使用带区域的-----------");
            Console.WriteLine(d.ToString("D",new CultureInfo("fr-FR")));
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            Console.WriteLine(string.Format("{0}:{1}.", culture.ToString(), d.ToString("D")));

            culture = new CultureInfo("es-ES");
            Thread.CurrentThread.CurrentCulture = culture;
            Console.WriteLine(string.Format("{0}:{1}.", culture.ToString(), d.ToString("D")));

        }
    }
}
