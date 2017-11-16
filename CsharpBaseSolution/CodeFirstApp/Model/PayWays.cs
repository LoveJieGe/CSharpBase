using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApp.Model
{
    [Table("PayWay")]
    public class PayWays
    {
        [Key]
        [Column("PwID")]
        public int PayWaysID { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public int DonateID { get; set; }
    }
}
