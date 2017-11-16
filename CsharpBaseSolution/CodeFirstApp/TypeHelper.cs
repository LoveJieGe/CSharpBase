using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApp
{
    public class TypeHelper
    {
        public static int GetTime(DateTime date)
        {
            int h = date.Hour;
            int m = date.Minute;
            return h * 100 + m;
        }
        public static int GetTime()
        {
            DateTime date = DateTime.Now;
            int h = date.Hour;
            int m = date.Minute;
            return h * 100 + m;
        }
    }
}
