using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Chapter28_CustomResource
{
    public class DataBaseResourceSet:ResourceSet
    {
        internal DataBaseResourceSet(string connectionString,CultureInfo cultureInfo)
            :base(new DataBaseResourceReader(connectionString,cultureInfo))
        { }
        public override Type GetDefaultReader()
        {
            return typeof(DataBaseResourceReader);
        }
    }
}
