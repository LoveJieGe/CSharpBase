using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11_ProCsharp.ProCsharp.Linq
{
    public class LinqProgram
    {
        /// <summary>
        /// 使用linq查询
        /// </summary>
        public static void LinqQuery()
        {
            var query = from r in Formula1.GetChampion()
                        where r.Country == "UK"
                        orderby r.Wins descending
                        select r;
            foreach (var racer in query)
            {
                Console.WriteLine(string.Format("{0:A}",racer));
            }
        }
        /// <summary>
        /// 使用扩展方法
        /// </summary>
        public static void ExtensionMethods()
        {
            var champions = new List<Racer>(Formula1.GetChampion());
            IEnumerable<Racer> championsEnumerable = champions.Where(r => r.Country == "UK").OrderByDescending(r => r.Wins);
            foreach (Racer racer in championsEnumerable)
            {
                Console.WriteLine(string.Format("{0:A}", racer));
            }
        }
        /// <summary>
        /// 筛选，可以合并多个表达式
        /// </summary>
        public static void LinqFilter()
        {
            var champions = from r in Formula1.GetChampion()
                            where r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Australia")
                            orderby r.Wins descending
                            select r;
            Console.WriteLine("----------------使用查询方法------------------");
            foreach (Racer racer in champions)
            {
                Console.WriteLine(string.Format("{0:A}", racer));
            }
            var list = new List<Racer>(Formula1.GetChampion());
            var championsEx = list.Where(r => r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Brazil")).OrderByDescending(r => r.Wins);
            Console.WriteLine("----------------使用扩展方法------------------");
            foreach (Racer racer in championsEx)
            {
                Console.WriteLine(string.Format("{0:A}", racer));
            }
        }
        /// <summary>
        /// 复合的from查询
        /// </summary>
        public static void LinqManyFrom()
        {
            var drivers = from r in Formula1.GetChampion()
                          from c in r.Cars
                          where c == "Ferrari"
                          orderby r.LastName
                          select r.FirstName + " " + r.LastName;
            Console.WriteLine("----------------使用查询方法------------------");
            foreach (string name in drivers)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("----------------使用扩展方法------------------");
            var list = new List<Racer>(Formula1.GetChampion());
            IEnumerable<string> names = list.SelectMany(r => r.Cars, (r, c) => new { Racer = r, Car = c }).Where(r => r.Car == "Ferrari").OrderBy(r => r.Racer.LastName).Select(a=>a.Racer.FirstName+" "+a.Racer.LastName);
            foreach (string name in names)
            {
                Console.WriteLine(name);
            }
        }
        /// <summary>
        /// 根据一个关键字对查询结果进行分组
        /// </summary>
        public static void LinqGroupBy()
        {
            var countries = from r in Formula1.GetChampion()
                            group r by r.Country into g
                            orderby g.Count() descending, g.Key
                            where g.Count() > 2
                            select new
                            {
                                Country = g.Key,
                                Count = g.Count()
                            };
            Console.WriteLine("----------------使用查询方法------------------");
            foreach (var item in countries)
            {
                Console.WriteLine(item.Country + "\t" + item.Count);
            }
            Console.WriteLine("----------------使用扩展方法------------------");
            var list = new List<Racer>(Formula1.GetChampion());
            var countriesEx = list.GroupBy(r => r.Country).Where(g => g.Count() > 2).OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key).Select(g => new { Country = g.Key, Count = g.Count() });
            foreach (var item in countriesEx)
            {
                Console.WriteLine("{0,-10}  {1}",item.Country,item.Count);
            }
        }

    }
}
