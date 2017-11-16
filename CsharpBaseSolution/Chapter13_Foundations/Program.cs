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
            Say();                             //由于Main不能使用async标记
            Console.ReadLine();
        }
        private async static void Say()
        {
            var t = TestAsync();
            Thread.Sleep(1100);                                     //主线程在执行一些任务
            Console.WriteLine("Main Thread");                       //主线程完成标记
            Console.WriteLine(await t);                             //await 主线程等待取异步返回结果
        }
        static async Task<string> TestAsync()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(1000);                                 //异步执行一些任务
                return "Hello World";                               //异步执行完成标记
            });
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
