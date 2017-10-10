using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11_ProCsharp.ProCsharp.Linq
{
    public class LinqProgram
    {
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
    }
}
