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

        /// <summary>
        /// 带嵌套的查询分组
        /// </summary>
        public static void LinqGroupByNested()
        {
            var countries = from r in Formula1.GetChampion()
                            group r by r.Country into g
                            orderby g.Count() descending, g.Key
                            where g.Count() >= 2
                            select new
                            {
                                Country = g.Key,
                                Count = g.Count(),
                                Racers = from r1 in g
                                         orderby r1.LastName
                                         select r1.FirstName+" "+r1.LastName
                            };
            Console.WriteLine("----------------使用查询方法------------------");
            foreach (var item in countries)
            {
                Console.WriteLine(item.Country + "\t" + item.Count);
                foreach (string name in item.Racers)
                {
                    Console.WriteLine(name);
                }
            }
            Console.WriteLine("----------------使用扩展方法------------------");
            var list = new List<Racer>(Formula1.GetChampion());
            var countriesEx = list.GroupBy(r => r.Country).Where(g => g.Count() > 2).OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key).Select(g => new { 
                    Country = g.Key, 
                    Count = g.Count(),
                    Racers = g.OrderBy(r1=>r1.LastName)
                .Select(r1=>(r1.FirstName+" "+r1.LastName))});
            foreach (var item in countriesEx)
            {
                Console.WriteLine("{0,-10}  {1}", item.Country, item.Count);
                foreach (string name in item.Racers)
                {
                    Console.WriteLine(name);
                }
            }
        }
        /// <summary>
        /// 内连接
        /// </summary>
        public static void LinqJoin()
        {
            //赛车手
            var racers = from r in Formula1.GetChampion()
                         from y in r.Years
                         select new
                         {
                             Year = y,
                             Name = r.FirstName + " " + r.LastName
                         };
            var teams = from t in Formula1.GetContructorChampion()
                        from y in t.Years
                        select new { Year = y, Name = t.Name };
            Console.WriteLine("----------------内连接------------------");
            var racersAndTeams = (from r in racers
                                 join t in teams on r.Year equals t.Year
                                 orderby r.Year descending
                                 select new
                                 {
                                     r.Year,
                                     Champion = r.Name,
                                     Constructor = t.Name
                                 }).Take(10);
            foreach (var item in racersAndTeams)
            {
                Console.WriteLine("{0}  {1:-20} {2}", item.Year, item.Champion,item.Constructor);
            }
            Console.WriteLine("----------------左外连接------------------");
            var racersAndTeamsLeft = (from r in racers
                                  join t in teams on r.Year equals t.Year into rt
                                  from t2 in rt.DefaultIfEmpty()
                                  select new
                                  {
                                      Year=r.Year,
                                      Champion = r==null?"空选手":r.Name,
                                      Constructor = t2==null?"空值":t2.Name
                                  }).Take(10);
            foreach (var item in racersAndTeamsLeft)
            {
                Console.WriteLine("{0}  {1:-20} {2}", item.Year, item.Champion, item.Constructor);
            }
        }
        /// <summary>
        /// 集合合并
        /// </summary>
        public static void LinqZip()
        {
            var racersName = from r in Formula1.GetChampion()
                             where r.Country == "Italy"
                             orderby r.Wins descending
                             select new
                             {
                                 Name = r.FirstName + " " + r.LastName
                             };
            var racerNameAndStart = from r in Formula1.GetChampion()
                                    where r.Country == "Italy"
                                    orderby r.Wins descending
                                    select new
                                    {
                                        FirstName = r.FirstName,
                                        Starts = r.Starts
                                    };
            var racers = racersName.Zip(racerNameAndStart, (first, second) => first.Name + "Starts:" + second.Starts);

            foreach (var name in racers)
            {
                Console.WriteLine(name);
            }
        }
        /// <summary>
        /// 聚合操作符
        /// </summary>
        public static void LinqCount()
        {
            var query = from r in Formula1.GetChampion()
                        let numberYear = r.Years.Count()
                        where numberYear >= 3
                        select new
                        {
                            Name = r.FirstName + " " + r.LastName,
                            Times = numberYear
                        };
            foreach (var item in query)
            {
                Console.WriteLine(item.Name + "次数：" + item.Times);
            }
        }
        /// <summary>
        /// 并行执行
        /// </summary>
        public static void LinqParallel()
        {
            const int arraySize = 100000000;
            var r = new Random();
            var data = Enumerable.Range(0, arraySize).Select(x => r.Next(140)).ToList();
            var res = data.AsParallel().Where(x => Math.Log(x) < 4).Select(x => x).Average();
        }

    }
}
