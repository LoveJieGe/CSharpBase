using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Drawing;

namespace Chapter28_CreateResource
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ResXResourceWriter rw = new ResXResourceWriter("Demo.resx");
            using (Image image = Image.FromFile("南八102.jpg"))
            {
                rw.AddResource("Image", image);
                rw.AddResource("Title", "c#高级编程");
                rw.AddResource("Chapter", "第28张");
                rw.AddResource("Author", "宋涛杰");
                rw.Close();
            }
        }
    }
}
