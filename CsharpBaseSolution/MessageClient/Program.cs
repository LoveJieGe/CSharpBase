using MessageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MessageClient
{
    class Program
    {
        private class ClientCallBack : IMyMessageCallBack
        {
            public void OnCallBack(string message)
            {
                Console.WriteLine("message from server:"+message);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("客户端等待服务器端");
            Console.ReadLine();
            DuplexSample();
            Console.WriteLine("客户端退出");
            Console.ReadLine();
        }

        private async static void DuplexSample()
        {
            WSDualHttpBinding binding = new WSDualHttpBinding();
            var address = new EndpointAddress("http://localhost:8733/Design_Time_Addresses/MessageService/MessageService/");
            var context = new InstanceContext(new ClientCallBack());
            var factory = new DuplexChannelFactory<IMyMessage>(context, binding, address);
            IMyMessage message = factory.CreateChannel();
            await Task.Run(() => { message.MessageToServer("from the server"); });
        }
    }
}
