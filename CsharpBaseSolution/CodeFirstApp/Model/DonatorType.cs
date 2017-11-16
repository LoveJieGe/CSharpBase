using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApp.Model
{
    public class DonatorType
    {
        public int DonatorTypeID { get; set; }
        public string DonatorTypeName { get; set; }
        public ICollection<Donators> Donators { get; set; }
    }
    public class DonatorTypeMap:EntityTypeConfiguration<DonatorType>
    {
        public DonatorTypeMap()
        {
            HasMany(d => d.Donators).WithOptional(d => d.DonatorType).WillCascadeOnDelete(false);
        }
    }
}
