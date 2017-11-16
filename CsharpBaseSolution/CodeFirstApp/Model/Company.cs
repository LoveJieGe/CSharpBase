using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApp.Model
{
    public class Company
    {
        public Company()
        {
            Persons = new HashSet<Person>();
        }
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public ICollection<Person> Persons { get; set; }
    }
}
