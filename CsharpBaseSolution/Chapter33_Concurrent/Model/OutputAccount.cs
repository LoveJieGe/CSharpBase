using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter33_Concurrent.Model
{
    [Table("OutputAccount")]
    public class OutputAccount
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }

    [Table("InputAccount")]
    public class InputAccount
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}
