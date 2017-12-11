using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HostOne
{
    class Program
    {
        internal static ServiceHost host = null;
        static void Main(string[] args)
        {
            string host = "host A";
            DemoService.DemoService.Server = host;
            StartService();
            Console.WriteLine("{0} 正在运行，请按退出键退出", host);
            Console.ReadLine();
            StopService();
        }
        internal static void StartService()
        {
            try
            {
                if (host == null)
                    host = new ServiceHost(typeof(DemoService.DemoService));
                host.Open();
            }
            catch
            {
                Console.WriteLine("请使用管理员身份打开应用程序");
            }
        }
        internal static void StopService()
        {
            if (host != null && host.State == CommunicationState.Opened)
                host.Close();
        }
    }
}
