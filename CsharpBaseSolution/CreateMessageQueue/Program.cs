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
                else
                {
                    MessageQueue[] queues = MessageQueue.GetPrivateQueuesByMachine(Environment.MachineName);
                    if (queues.Length > 0)
                    {
                        queue = queues[0];
                    }
                }
                if (queue != null)
                {
                    //queue.Authenticate = true;
                    Console.WriteLine("Queue Created:");
                    Console.WriteLine("Path:{0}", queue.Path);
                    Console.WriteLine("FormatName:{0}", queue.FormatName);
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

        }
    }
}
