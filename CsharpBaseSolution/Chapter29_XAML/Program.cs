using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Chapter29_XAML
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var b = new Button()
            {
                Content="点击",
                Width =100,Height=50,
            };
            b.Click += B_Click;
            var win = new Window() { Content = b, Title = "测试" };
            var app = new Application();
            app.Run(win);
        }

        private static void B_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("我被点击了");
        }
    }
}
