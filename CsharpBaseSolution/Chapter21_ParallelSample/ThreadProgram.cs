using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter21_ParallelSample
{
    public class ThreadProgram
    {
        public static void ThreadSample()
        {
            var t1 = new Thread(() =>
            {
                Console.WriteLine("线程1启动");
            });
            t1.Start();
        }

        public static void ThreadWithParameter(object o)
        {
            Data d = (Data)o;
            Console.WriteLine("运行一个线程，接收参数：{0}",d.Message);
        }
        public static void StartThreadWithParameter()
        {
            Data d = new Data { Message = "这是一个参数" };
            var t1 = new Thread(ThreadWithParameter);
            t1.Start(d);
        }
    }

    public struct Data
    {
        public string Message;
    }
}
