using CourseOrderServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CourseOrderHost
{
    class Program
    {
        internal static ServiceHost myServiceHost = null;
        /// <summary>
        /// 启动服务
        /// </summary>
        internal static void StartService()
        {
            try
            {
                myServiceHost = new ServiceHost(typeof(CourseOrderService), new Uri("http://localhost:9000/RoomReservation"));
                myServiceHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
                myServiceHost.Open();
            }
            catch (AddressAccessDeniedException)
            {
                Console.WriteLine("以提升的管理员模式启动Visual Studio或使用netsh.exe注册侦听器端口");
                return;
            }

        }
        internal static void StopService()
        {
            if (myServiceHost != null && myServiceHost.State == CommunicationState.Opened)
            {
                myServiceHost.Close();
            }
        }
        static void Main(string[] args)
        {
            StartService();
            Console.WriteLine("服务已启动，按任意键关闭服务!");
            Console.ReadLine();
            StopService();

        }
    }
}
