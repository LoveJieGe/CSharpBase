using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter15_ReflectionLib
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class FieldNameAttribute : Attribute
    {
        private string name;
        public FieldNameAttribute(string name)
        {
            this.name = name;
        }
        private string comment;
        public string Comment
        {
            get { return this.comment; }
            set { this.comment = value; }
        }

    }
}
