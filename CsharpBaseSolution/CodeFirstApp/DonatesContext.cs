using CodeFirstApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApp
{
    public class DonatesContext:DbContext
    {
        public DonatesContext():base("name=CodeFirstApp")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DonatesContext>());
        }
        public DbSet<Donators> Donators { get; set; }
        public DbSet<PayWays> PayWays { get; set; }
        public DbSet<DonatorType> DonatorType { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Company> Company { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DonatesMap());
            modelBuilder.Configurations.Add(new DonatorTypeMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
