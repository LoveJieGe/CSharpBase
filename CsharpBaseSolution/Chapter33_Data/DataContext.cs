using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter33_Data
{
    public class DataContext:DbContext
    {
        public DataContext() : base("name=ConnectionString")
        { }
        public DataContext(string name):base(string.Format("name={0}",name))
        { }
        public DbSet<Donator> Donator { get; set; }
    }
}
