using Chapter33_Concurrent.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter33_Concurrent
{
    public class DonateContext:DbContext
    {
        public DonateContext():base("name=ConnectionString")
        {
            
        }
        public DbSet<Donator> Donator { get; set; }
        public DbSet<OutputAccount> OutputAccount { get; set; }
        public DbSet<InputAccount> InputAccount { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donator>().Property(d => d.RowVersion).IsRowVersion();
            base.OnModelCreating(modelBuilder);
        }
    }
}
