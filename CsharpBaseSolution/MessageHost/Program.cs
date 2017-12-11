
using MessageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MessageHost
{
    class Program
    {
        internal static ServiceHost serviceHost = null;
        internal static void StartService()
        {
            serviceHost = new ServiceHost(typeof(MessageService.MyMessageService));
            serviceHost.Open();
        }
        internal static void StopService()
        {
            if (serviceHost.State == CommunicationState.Opened)
                serviceHost.Close();
        }
        static void Main(string[] args)
        {
            StartService();
            Console.WriteLine("service run,press return to exit");
            Console.ReadLine();
            StopService();
            Console.WriteLine("service stop");
        }
    }
}
