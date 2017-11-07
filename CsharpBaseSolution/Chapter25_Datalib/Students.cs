using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter25_Datalib
{
    [Serializable]
    public class Students
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
