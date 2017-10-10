using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chapter11_ProCsharp.ProCsharp.Linq
{
    [Serializable]
    public class Racer:IComparable<Racer>,IFormattable
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public int Starts { get; set; }
        public int Wins { get; set; }
        //赛车手获取冠军的年份
        public IEnumerable<int> Years { get; private set; }
        //赛车手在获得冠军的年份中使用的车型
        public IEnumerable<string> Cars { get; private set; }
        public Racer(string firstName, string lastName, string country,int starts, int wins,IEnumerable<int> years,IEnumerable<string> cars)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Country = country;
            this.Starts = starts;
            this.Wins = wins;
            this.Years = new List<int>(years);
            this.Cars = new List<string>(cars);
        }
        public Racer(string firstName, string lastName, string country,int starts,int wins) : this(firstName, lastName, country,starts, wins,null,null) { }
        public int CompareTo(Racer other)
        {
            if (other == null) return -1;
            int compare = string.Compare(this.LastName, other.LastName);
            if (compare == 0)
                return string.Compare(this.FirstName, other.FirstName);
            return compare;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format)) format = "N";
            switch (format.ToUpper())
            {
                case "N":
                    return ToString();
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                case "W":
                    return string.Format("{0} Wins:{1}", ToString(), Wins);
                case "C":
                    return string.Format("{0} Country:{1}", ToString(), Country);
                case "A":
                    return string.Format("{0} Wins:{1}, Country:{2}", ToString(), Wins, Country);
                default:
                    throw new FormatException(string.Format(formatProvider, "Format {0} is not support", format));
            }
        }
        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
        public string ToString(string format)
        {
            return ToString(format, null);
        }
    }
}
