using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Chapter28_ResourceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ResourceManager rm = new ResourceManager("Chapter28_ResourceDemo.Demo", Assembly.GetExecutingAssembly());
            Console.WriteLine(rm.GetString("Title"));
            Console.WriteLine(rm.GetString("Chapter"));
            Console.WriteLine(rm.GetString("Author"));
            Image image = (Image)rm.GetObject("Image");
            image.Save("南八.bmp");
            Console.ReadKey();
        }
        private static void StronglyTypeResource()
        {
            Console.WriteLine(Demo)
        }
    }
}
