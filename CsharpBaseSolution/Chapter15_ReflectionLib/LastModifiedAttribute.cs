using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter15_ReflectionLib
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true,Inherited =false)]
    public class LastModifiedAttribute:Attribute
    {
        private readonly DateTime _dateModified;
        private readonly string _change;
        public LastModifiedAttribute(string dateModified, string change)
        {
            this._dateModified = DateTime.Parse(dateModified);
            this._change = change;
        }

        public DateTime DateModified
        {
            get { return this._dateModified; }
        }
        public string Change
        {
            get { return this._change; }
        }
        public string Issues
        {
            get;set;
        }
    }
    [AttributeUsage(AttributeTargets.Assembly)]
    public class SupportsWhatsNewAttribute : Attribute
    {

    }
}
