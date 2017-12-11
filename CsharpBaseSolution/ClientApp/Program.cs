using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("client---wait for services");
            Console.ReadLine();
            DemoService.DemoServiceClient client = new DemoService.DemoServiceClient();
            Console.WriteLine(client.GetData("Hello"));
            Console.ReadLine();
        }
    }
}
