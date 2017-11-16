using CodeFirstApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApp.Model
{
    public class DonatesMap:EntityTypeConfiguration<Donators>
    {
        public DonatesMap()
        {
            ToTable("Donators");
            Property(d => d.Name).IsRequired().HasMaxLength(10);
            HasMany(d => d.PayWays).WithRequired().HasForeignKey(p => p.DonateID);
        }

    }
}
