using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseOrderReceiver
{
    public class MessageInfo
    {
        public  string ID
        {
            get;set;
        }
        public string Label
        {
            get;set;
        }
        public override string ToString()
        {
            return Label;
        }

    }
}
