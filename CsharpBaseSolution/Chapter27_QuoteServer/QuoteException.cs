using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Chapter27_QuoteServer
{
    public class QuoteException:Exception
    {
        public QuoteException() { }
        public QuoteException(string message) : base(message) { }
        public QuoteException(string message,Exception exception):base(message,exception)
        { }
        public QuoteException(SerializationInfo info,StreamingContext context):base(info,context)
        { }
    }
}
