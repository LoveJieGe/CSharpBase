using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11_ProCsharp.ProCsharp.Linq
{
    public static class StringExtension
    {
        public static string FirstName(this string name)
        {
            int index = name.LastIndexOf(' ');
            return name.Substring(0, index);
        }
        public static string LastName(this string name)
        {
            int index = name.LastIndexOf(' ');
            return name.Substring(index+1);
        }
    }
}
