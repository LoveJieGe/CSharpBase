using CodeFirstDemo.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDemo.Context
{
    public class DonatorsContext:DbContext
    {
        public DonatorsContext():base("name=ConnectionString")
        {
        }

        public virtual DbSet<Donator> Donator { get; set; }
    }
}
