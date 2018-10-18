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
            //ParallelInvoke();
            //TaskMethod("测试");
            //TaskUsingThreadPool();
            //StartTaskWithResult();
            //CancellationTokenSource();
            //CancellationTokenSourceTask();
            //ThreadPoolProgram();
            //Console.WriteLine("主线程");
            ////ThreadProgram.ThreadSample();
            //ThreadProgram.StartThreadWithParameter();

            Counter c = new Counter(3);
            c.ThresholdReached += c_ThresholdReached;

            Console.WriteLine("press 'a' key to increase total");
            while (Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("adding one");
                c.Add(1);
            }
            Console.ReadKey();
        }
        static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine("The threshold of {0} was reached at {1}.", e.Threshold, e.TimeReached);
            // Environment.Exit(0);
        }
        static void ParallelParagram()
        {
            ParallelLoopResult result = Parallel.For(0, 10, (i) =>
            {
                Console.WriteLine("编号：{0},Task:{1},ThreadL{2}", i, 
                    Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            });
            Console.WriteLine("线程完成：{0}", result.IsCompleted);
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
            ParallelLoopResult result = Parallel.For(0, 10, 
                async (int i, ParallelLoopState pls) =>
            {
                Console.WriteLine("编号：{0},Task:{1},ThreadL{2}", i, Task.CurrentId, 
                    Thread.CurrentThread.ManagedThreadId);
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
            string[] data = { "java", "csharp", "c++", "c", "php" };
            ParallelLoopResult result = Parallel.ForEach<string>(data, (str, state, i) =>
            {
                Console.WriteLine("迭代次数：{0},{1}", i, str);
                if (i > 3)
                    state.Break();
            });
            Console.WriteLine("是否完成:{0}", result.IsCompleted);
            Console.WriteLine("最低迭代:{0}", result.LowestBreakIteration);
            Thread.Sleep(3000);
            Console.WriteLine("Foo");

        }
        static void Boo()
        {
            string[] data = { "str1", "str2", "str3" };
            ParallelLoopResult result = Parallel.ForEach<string>(data, str =>
            {
                Console.WriteLine(str);
            });
            Console.WriteLine("是否完成:{0}", result.IsCompleted);
            Console.WriteLine("最低迭代:{0}", result.LowestBreakIteration);
            Console.WriteLine("Boo");
        }
        static object taskMethodLock = new object();
        static void TaskMethod(object title)
        {
            lock (taskMethodLock)
            {
                Console.WriteLine(title);
                Console.WriteLine("Task ID:{0},Thread:{1}", Task.CurrentId == null ? "no Task" : Task.CurrentId.ToString(), Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("是线程池的线程：{0}", Thread.CurrentThread.IsThreadPoolThread);
                Console.WriteLine("是后台线程：{0}", Thread.CurrentThread.IsBackground);
                Console.WriteLine();
            }
        }

        static void TaskUsingThreadPool()
        {
            var taskFactory = new TaskFactory();
            Task t1 = taskFactory.StartNew(TaskMethod, "使用任务线程池开启任务");
            Task t2 = Task.Factory.StartNew(TaskMethod, "使用 Task.Factory开启任务");
            Task t3 = new Task(TaskMethod, "使用Task的实例开启线程池");
            t3.Start();
            Task t4 = Task.Run(() => { TaskMethod("使用Task.Run开始任务"); });
        }
        static Tuple<int, int> TaskWithResult(object division)
        {
            Tuple<int, int> div = (Tuple<int, int>)division;
            int result = div.Item1 / div.Item2;
            int remider = div.Item1 % div.Item2;
            Console.WriteLine("创建了一个结果任务");
            return Tuple.Create<int, int>(result, remider);
        }
        static void StartTaskWithResult()
        {
            var t1 = new Task<Tuple<int, int>>(TaskWithResult, Tuple.Create<int, int>(12, 3));
            t1.Start();
            Console.WriteLine("输出结果：{0}", t1.Result);
            t1.Wait();
            Console.WriteLine("任务的结果：{0},{1}", t1.Result.Item1, t1.Result.Item2);
        }

        static void CancellationTokenSource()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() =>
            {
                Console.WriteLine("取消Cancellation");
            });
            cts.CancelAfter(500);
            try
            {
                Parallel.For(0, 100, new ParallelOptions(), (i) =>
                {
                    Console.WriteLine("loop_{0} 开启", i);
                    int sum = 0;
                    for (int j = 0; j < 100; j++)
                    {
                        Thread.Sleep(2);
                        sum += j;
                    }
                    Console.WriteLine("loop_{0} 结束", i);
                });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void CancellationTokenSourceTask()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(() =>
            {
                Console.WriteLine("取消Cancellation");
            });
            cts.CancelAfter(500);

            Task t1 = Task.Run(() =>
            {
                Console.WriteLine("in Task");

                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(50);
                    CancellationToken token = cts.Token;
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("取消被请求，任务已经取消");
                        token.ThrowIfCancellationRequested();
                        break;
                    }
                    Console.WriteLine("in loop");
                }
            }, cts.Token);
            try
            {
                t1.Wait();
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Exception:{0},{1}。", e.GetType().Name, e.Message);
                foreach (var innerException in e.InnerExceptions)
                {
                    Console.WriteLine("innerException:{0},{1}。", innerException.GetType().Name, innerException.Message);
                }
            }
        }

        static void ThreadPoolProgram()
        {
            int nWorkerThreads;
            int nCompletionPortThreads;
            ThreadPool.GetMaxThreads(out nWorkerThreads, out nCompletionPortThreads);
            Console.WriteLine("工作的最大线程：{0},I/OCompletation线程：{1}",nWorkerThreads,nCompletionPortThreads);
            for (int i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem(JobForAThread);
                Thread.Sleep(20);
            }
            
        }
        static void JobForAThread(object state)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("loop:{0},运行的线程池线程:{1}",i,Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(30);
            }
        }
    }
}
