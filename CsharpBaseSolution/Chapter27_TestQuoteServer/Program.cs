using Chapter27_QuoteServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter27_TestQuoteServer
{
    class Program
    {
        static void Main(string[] args)
        {
            QuoteServer server = new QuoteServer("Quotes.txt", 4567);
            server.Start();
            Console.WriteLine("按任意键停止线程");
            Console.ReadLine();
            server.Stop();
        }
    }
}
