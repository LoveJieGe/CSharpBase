using CodeFirstDemo.Context;
using CodeFirstDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //AddRecord();
            QueryRecord();
            Console.Read();
        }
        static void AddRecord()
        {
            List<Donator> list = new List<Donator>();
            for(int i = 0;i<10;i++)
            {
                Donator d = new Donator();
                d.Name = "Name_" + i;
                d.Amount = 10 + 2 * i;
                d.DonateDate = DateTime.Today.AddDays(0 - i);
                d.DonateTime = TypeHelper.GetTime();
                list.Add(d);
            }
            DonatorsContext context = new DonatorsContext();
            context.Donator.AddRange(list);
            context.SaveChanges();
            Console.WriteLine("操作成功");
        }
        static void QueryRecord()
        {
            DonatorsContext context = new DonatorsContext();
            var donators = from d in context.Donator
                           where d.Amount > 10
                           select d;
            foreach(var d in donators)
            {
                Console.WriteLine(d.Name + ":" + d.Amount);
            }
        }
    }
}
