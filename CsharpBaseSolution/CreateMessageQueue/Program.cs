using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CreateMessageQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateQueue();
            //ReadQueue();
            Console.Read();

        }

        private static void CreateQueue()
        {
            try
            {
                MessageQueue queue = null;
                string path = @".\Private$\MyNewPublicQueue";
                if (!MessageQueue.Exists(path))
                {
                    using (queue = MessageQueue.Create(path))
                    {
                        queue.Label = "Demo Queue";
                    }
                }
                queue = new MessageQueue(path);
                //queue.Send("Sample Message", "Label");
                //queue.Send("Admin", "UserName");
                queue.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
                //using (MessageEnumerator messages = queue.GetMessageEnumerator2())
                //{
                   
                //    while (messages.MoveNext(TimeSpan.FromMinutes(1)))
                //    {
                //        Message message = messages.Current;
                //        Console.WriteLine(message.Label+":"+message.Body);
                //    }
                //}
                Console.WriteLine("Queue Created:");
                Console.WriteLine("Path:{0}", queue.Path);
                Console.WriteLine("FormatName:{0}", queue.FormatName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void ReadQueue()
        {
            try
            {
                MessageQueue queue = null;
                string path = @".\Private$\MyNewPublicQueue";
                if (!MessageQueue.Exists(path))
                {
                    using (queue = MessageQueue.Create(path))
                    {
                        queue.Label = "队列测试";
                    }
                }
                queue = new MessageQueue(path);
                queue.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
                queue.ReceiveCompleted += Queue_ReceiveCompleted;
                queue.BeginReceive();
                Console.WriteLine("Queue Created:");
                Console.WriteLine("Path:{0}", queue.Path);
                Console.WriteLine("FormatName:{0}", queue.FormatName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void Queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            MessageQueue queue = sender as MessageQueue;
            Message message = queue.EndReceive(e.AsyncResult);
            Console.WriteLine(message.Body);
        }
    }
}
