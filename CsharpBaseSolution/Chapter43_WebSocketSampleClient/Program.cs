using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chapter43_WebSocketSampleClient.DemoService;
using System.ServiceModel;

namespace Chapter43_WebSocketSampleClient
{
    class Program
    {
        private class CallBackHandler : IDemoServiceCallback
        {
            public void SendMessage(string message)
            {
                Console.WriteLine("从服务器端接受的信息：" + message);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("客户端等待服务器端!");
            Console.ReadLine();
            StartSendMessage();
            Console.WriteLine("下一个返回退出");
            Console.ReadLine();
        }
        static async void StartSendMessage()
        {
            var callBackInstance = new InstanceContext(new CallBackHandler());
            var client = new DemoServiceClient(callBackInstance);
            await client.StartSendMessageAsync();
        }
    }
}
