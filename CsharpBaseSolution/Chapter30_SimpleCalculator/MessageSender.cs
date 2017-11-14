using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter30_SimpleCalculator
{
    public class MessageSender : IMessageSender
    {
        //[Export("Send",typeof(Action<string,int>))]
        //[ExportMetadata("speed","fast")]
        //public void Send(string message,int number)
        //{
        //    Console.WriteLine("导出+" + message + "数字+" + number);
        //}
        /// <summary>
        /// 现在可以使用SpeedExport替代Export和ExportMetadata了
        /// </summary>
        /// <param name="message"></param>
        /// <param name="number"></param>
        [SpeedExport("Send", typeof(Action<string, int>),Speed =Speed.Fast)]
        public void Send(string message, int number)
        {
            Console.WriteLine("导出+" + message + "数字+" + number);
        }
    }
    public class MessageSender2: IMessageSender
    {
        //[Export("Send", typeof(Action<string, int>))]
        //[ExportMetadata("speed", "slow")]
        //public void Send(string message, int number)
        //{
        //    Thread.Sleep(3000);
        //    Console.WriteLine("导出+" + message + "数字+" + number);
        //}
        [SpeedExport("Send", typeof(Action<string, int>), Speed = Speed.Slow)]
        public void Send(string message, int number)
        {
            Console.WriteLine("导出+" + message + "数字+" + number);
        }
    }
    public interface IMessageSender
    {
        void Send(string message, int number);
    }
    public class Processor: IProcessor
    {
        //[ImportMany("Send", typeof(Action<string,int>))]
        //public Lazy<Action<string, int>,IDictionary<string,object>>[] SendMethods{get;set;}
        //public Action<string,int> MessageSender { get; set; }
        /// <summary>
        /// 对于入口需要一个，包含元数据的接口，这样才能访问强类型的功能
        /// </summary>
        [ImportMany("Send", typeof(Action<string, int>))]
        public Lazy<Action<string, int>,ISpeedCapabilities>[] SendMethods { get; set; }
        public void Send()
        {
            foreach(var Method in  SendMethods)
            {
                if (Method.Metadata.Speed==Speed.Fast)
                    Method.Value("song", 12);
                else
                    Method.Value("jie", 12);
            }
            //MessageSender("song", 12);
        }
    }
    [InheritedExport]
    public interface IProcessor
    {
        void Send();
    }
}
