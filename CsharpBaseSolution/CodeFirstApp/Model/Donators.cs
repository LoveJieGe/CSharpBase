using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApp.Model
{

    public class Donators
    {
        public Donators()
        {
            PayWays = new HashSet<PayWays>();
        }
        public int DonatorsID { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonateDate { get; set; }
        public int DonateTime { get; set; }
        public virtual ICollection<PayWays> PayWays { get; set; }

        public int? DonatorTypeID { get; set; }
        public virtual DonatorType DonatorType { get; set; }
    }

}
