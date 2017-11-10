using Chapter28_ResourceDemo.Resource;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter28_ResourceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = R.G("R5013906:不");
            Console.WriteLine(result);
            CultureInfo info = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = info;
            result = R.G("R5013906:不");
            Console.WriteLine(result);
            info = new CultureInfo("zh-TW");
            Thread.CurrentThread.CurrentCulture = info;
            result = R.G("R5013906:不");
            Console.WriteLine(result);
            Console.ReadKey();
        }
        private static void ResourceDemo()
        {
            ResourceManager rm = new ResourceManager("Chapter28_ResourceDemo.Demo", Assembly.GetExecutingAssembly());
            Console.WriteLine(rm.GetString("Title"));
            Console.WriteLine(rm.GetString("Chapter"));
            Console.WriteLine(rm.GetString("Author"));
            Image image = (Image)rm.GetObject("Image");
            image.Save("南八.bmp");
            Console.ReadKey();
        }
    }
}
