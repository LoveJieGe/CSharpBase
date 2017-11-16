using CodeFirstApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new Initializer());
            CreateRecord();
            Console.Read();
        }

        static void CreateRecord()
        {
            var context = new DonatesContext();
            //context.Dispose();
            Donators d = new Donators();
            d.Name = "赵丽颖";
            d.Amount = 10;
            DateTime now = DateTime.Now;
            d.DonateDate = DateTime.Today;
            d.DonateTime = TypeHelper.GetTime(now);
            context.Donators.Add(d);
            d.PayWays.Add(new PayWays() { Name = "支付宝" });
            d.PayWays.Add(new PayWays() { Name = "微信" });
            d.DonatorType = new DonatorType() { DonatorTypeName = "非博客园" };

            Person p = new Person() { Name = "乔布斯", IsActive = false };
            Person p2 = new Person() { Name = "比尔盖茨", IsActive = false };
            context.Person.Add(p);
            context.Person.Add(p2);
            Company c = new Company();
            c.Name = "微软";
            c.Persons.Add(p2);
            context.Company.Add(c);
            context.SaveChanges();
            Console.WriteLine("操作成功");
        }
        static void FindRecord()
        {
            var context = new DonatesContext();
            var donators =context.Donators;
            foreach(Donators d in donators)
            {
                Console.WriteLine("ID:{0}\t姓名:{1}\t", d.DonatorsID, d.Name);
            }
        }
        static void UpdateRecord()
        {
            var context = new DonatesContext();
            var donators = context.Donators;
            if(donators.Any())
            {
                Donators d = donators.First(r => r.Name == "胡歌");
                d.Name = "周杰伦";
                context.SaveChanges();
            }
            Console.WriteLine("修改成功");
        }

    }
}
