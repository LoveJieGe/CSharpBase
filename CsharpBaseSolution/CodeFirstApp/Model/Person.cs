using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApp.Model
{
    public class Person
    {
        public Person()
        {
            Companies = new HashSet<Company>();
        }
        public int PersonID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
    public class PersonMap:EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            HasMany(p => p.Companies)
                .WithMany(c => c.Persons)
                .Map(m =>
                {
                    m.MapLeftKey("PersonID");
                    m.MapRightKey("CompanyID");
                });
        }
    }
}
