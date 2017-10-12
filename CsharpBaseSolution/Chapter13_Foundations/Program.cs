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
            string name = "胡歌";
            //var result = Greeting(name);
            //var result = GreetingAsync(name);
            //Console.WriteLine(result.Result);
            CallerWaitAsync();
            Console.ReadKey();
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
            return Task.Run<string>(()=>Greeting(name));
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
