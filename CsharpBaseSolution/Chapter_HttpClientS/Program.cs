using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter26_HttpClientS
{
    class Program
    {
        static void Main(string[] args)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
            string s = key.GetValue("").ToString();
            int index = s.LastIndexOf('.');
            System.Diagnostics.Process process = new System.Diagnostics.Process();
           // Console.WriteLine();
            process.StartInfo.FileName = string.Format(@"{0}",s.Substring(0, index + 4));
            string url = "http://www.baidu.com?java";
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.Arguments = string.Format("--new-window --window-size==300*500  --window-position==300,200  \"{0}\"", url);
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
            process.Start();
            process.WaitForInputIdle();
            //Console.ReadKey();
        }
    }
}
