using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter21_ParallelSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //ParallelParagram();
            //ParallelParagramAsync();
            //ParallelParagramBreak();
            ParallelInvoke();
            Console.ReadKey();
        }
        static void ParallelParagram()
        {
            ParallelLoopResult result = Parallel.For(0, 10, (i) =>
            {
                Console.WriteLine("编号：{0},Task:{1},ThreadL{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            });
            Console.WriteLine("线程完成：{0}",result.IsCompleted);
        }
        static void ParallelParagramAsync()
        {
            ParallelLoopResult result = Parallel.For(0, 10, async (i) =>
            {
                Console.WriteLine("编号：{0},Task:{1},ThreadL{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                await Task.Delay(1000);
                Console.WriteLine("编号：{0},Task:{1},ThreadL{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            });
            Console.WriteLine("完成：{0}", result.IsCompleted);
        }
        /// <summary>
        /// 可以停止
        /// </summary>
        static void ParallelParagramBreak()
        {
            ParallelLoopResult result = Parallel.For(0, 10, async (int i,ParallelLoopState pls) =>
            {
                Console.WriteLine("编号：{0},Task:{1},ThreadL{2}", i, Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                await Task.Delay(10);
                if (i > 5)
                    pls.Break();
            });
            Console.WriteLine("完成：{0}", result.IsCompleted);
            Console.WriteLine("暂停的位置：{0}", result.LowestBreakIteration);
        }
        static void ParallelInvoke()
        {
            Parallel.Invoke(Foo, Boo);
        }
        static void Foo()
        {
            Thread.Sleep(3000);
            Console.WriteLine("Foo");
            
        }
        static void Boo()
        {
            Console.WriteLine("Boo");
        }  
    }
}
