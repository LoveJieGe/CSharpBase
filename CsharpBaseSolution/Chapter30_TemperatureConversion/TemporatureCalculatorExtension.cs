using Chapter30_CalculatorUtils;
using Chapter30Lib_CalculatorContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Chapter30_TemperatureConversion
{
    [CalculatorExtensionExport(typeof(ICalculatorExtension),
        Title = "温度转换",
        Description = "将摄氏温度转换成华氏温度，将华氏温度转换成摄氏温度",
        ImageUrl = "Temperature.png")]
    public class TemporatureCalculatorExtension : ICalculatorExtension
    {
        private FrameworkElement control;
        public FrameworkElement UI
        {
            get
            {
                return control ?? (new TemperatureConversion());
            }
        }
        private BitmapImage image;
        public BitmapImage Image
        {
            get
            {
                if(image==null)
                {
                    image = new BitmapImage();
                    image.BeginInit();
                    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Chapter30_TemperatureConversion.Images.Temperature.png");
                    image.StreamSource = stream;
                    image.EndInit();
                }
                return image;
            }
        }
    }
}
