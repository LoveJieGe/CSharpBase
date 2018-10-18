using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.IO;

namespace NPOITest
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = @"C:\Users\songtaojie\Downloads\总分类账_20180522_160534.xls";
            //NPOIExport.ReadFile(path);
            string path = @"image/mine.jpg";
            //Image image = Image.FromFile(path);
            //string ext = GetExtension(image);
            string fileName = Path.GetExtension(path);
            Console.WriteLine(fileName);
            Console.WriteLine(DateTime.Now.ToFileTime());
            //foreach (KeyValuePair<String, ImageFormat> par in GetImageFormats())
            //{
            //    Console.WriteLine(par.Key);
            //}
            Console.ReadLine();
        }

        private static Dictionary<String, ImageFormat> GetImageFormats()
        {
            var dic = new Dictionary<String, ImageFormat>();
            var properties = typeof(ImageFormat).GetProperties(BindingFlags.Static | BindingFlags.Public);
            foreach (var property in properties)
            {
                var format = property.GetValue(null, null) as ImageFormat;
                if (format == null) continue;
                dic.Add(("." + property.Name).ToLower(), format);
            }
            return dic;
        }
        public static String GetExtension(Image image)
        {
            var ImageFormats  = GetImageFormats();
            foreach (var pair in ImageFormats)
            {
                if (pair.Value.Guid == image.RawFormat.Guid)
                {
                    return pair.Key;
                }
            }
            throw new BadImageFormatException();
        }
    }
}
