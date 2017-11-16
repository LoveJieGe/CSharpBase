using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstApp
{
    public class Initializer:DropCreateDatabaseIfModelChanges<DonatesContext>
    {
        protected override void Seed(DonatesContext context)
        {
            for(int i = 0;i<10;i++)
            {
                context.Person.Add(new Model.Person() { Name = "Name" + i, IsActive = false });
            }
            base.Seed(context);
        }
    }
}
