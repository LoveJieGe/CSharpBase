using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chapter27_QuoteServer
{
    public class QuoteServer
    {
        private TcpListener listener;
        private int port;
        private string fileName;
        private List<string> quotes;
        private Random random;
        private Task listenerTask;
        public QuoteServer(string fileName,int port)
        {
            //Contract.Requires<ArgumentNullException>(fileName != null);
            //Contract.Requires<ArgumentException>(port >= IPEndPoint.MinPort && port < IPEndPoint.MaxPort);
            if (fileName == null)
                throw new ArgumentNullException();
            if (!(port >= IPEndPoint.MinPort && port < IPEndPoint.MaxPort))
                throw new ArgumentException();
            this.fileName = fileName;
            this.port = port;
        }
        public QuoteServer(string fileName):this(fileName,7890)
        {
            
        }
        public QuoteServer():this("quotes.txt")
        {
            
        }
        /// <summary>
        /// 一个辅助方法，从构造函数指定的文件中读取引用添加到quotes中
        /// </summary>
        protected void ReadQuotes()
        {
            try
            {
                quotes = File.ReadAllLines(fileName).ToList();
                if (quotes.Count == 0)
                    throw new QuoteException("Quotes为空!");
                random = new Random();
            }
            catch(IOException e)
            {
                throw new Exception("I/O Error:" + e);
            }
        }
        /// <summary>
        /// 返回集合中的一个随机引用
        /// </summary>
        protected string GetRandomQuoteOfTheDay()
        {
            int index = random.Next(0, quotes.Count);
            return quotes[index];
        }
        /// <summary>
        /// 启动任务
        /// </summary>
        public void Start()
        {
            ReadQuotes();
            listenerTask = Task.Factory.StartNew(Listener, TaskCreationOptions.LongRunning);
        }
        /// <summary>
        /// 关闭任务
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
        /// <summary>
        /// 恢复
        /// </summary>
        public void Suspend()
        {
            listener.Stop();
        }
        public void Resume()
        {
            Start();
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        public void RefreshQuotes()
        {
            ReadQuotes();
        }
        protected void Listener()
        {
            try
            {
                IPAddress iPAddress = IPAddress.Any;
                listener = new TcpListener(iPAddress, port);
                listener.Start();
                while(true)
                {
                    Socket clientSocket = listener.AcceptSocket();
                    string message = this.GetRandomQuoteOfTheDay();
                    var encoder = new UnicodeEncoding();
                    byte[] buffer = encoder.GetBytes(message);
                    clientSocket.Send(buffer, buffer.Length, 0);
                    clientSocket.Close();
                }
            }
            catch(SocketException e)
            {
                Trace.TraceError(string.Format("QuoteServer:{0}", e.Message));
                throw new QuoteException("Quote Error:", e);
            }
        }
    }
}
