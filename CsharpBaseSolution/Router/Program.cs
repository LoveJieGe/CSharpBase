using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Routing;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    class Program
    {
        internal static ServiceHost host = null;
        static void Main(string[] args)
        {
            StartService();

            Console.WriteLine("Router is running. Press return to exit");
            Console.ReadLine();

            StopService();
        }
        internal static void StartService()
        {
            try
            {
                host = new ServiceHost(typeof(RoutingService));
                host.Faulted += Host_Faulted;
                host.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("请使用管理员身份启动应用程序");
            }
        }

        private static void Host_Faulted(object sender, EventArgs e)
        {
            Console.WriteLine("routing error");
        }
        internal static void StopService()
        {
            if (host != null && host.State == CommunicationState.Opened)
                host.Close();
        }
    }
}
