using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Chapter13_Foundations
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(Test) { IsBackground = false }.Start();      //.Net 在1.0的时候，就已经提供最基本的API.
            ThreadPool.QueueUserWorkItem(o => Test());              //线程池中取空闲线程执行委托（方法）
            Task.Run((Action)Test);                                 //.Net 4.0以上可用
            Console.WriteLine("Main Thread");
            Console.ReadLine();
        }

        static void Test()
        {
            Thread.Sleep(1000);
            Console.WriteLine("Hello World");
        }
        /// <summary>
        /// 同步执行的方法
        /// </summary>
        /// <param name="name"></param>
        static string Greeting(string name)
        {
            Thread.Sleep(3000);
            return string.Format("Hello,{0}",name);
        }
        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static Task<string> GreetingAsync(string name)
        {
            var result =  Task.Run<string>(()=>Greeting(name));
            Console.WriteLine(result.Result);
            return result;
        }
        private async static void CallerWaitAsync()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string result = await GreetingAsync("赵丽颖");
            string result2 = await GreetingAsync("刘亦菲");
            //var result =  GreetingAsync("赵丽颖");
            //var result2 =  GreetingAsync("刘亦菲");
            sw.Stop();
            Console.WriteLine("Hello,{0}+{1},时间：{2}", result, result2,sw.Elapsed);
        }
    }
}
