using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Chapter28_CustomResource
{
    public class DataBaseResourceManager:ResourceManager
    {
        private string connectionString;
        private Dictionary<string, DataBaseResourceSet> resourceSets;
        internal DataBaseResourceManager(string connectionString)
        {
            this.connectionString = connectionString;
            resourceSets = new Dictionary<string, DataBaseResourceSet>();
        }
        protected override ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
        {
            DataBaseResourceSet resourceSet = null;
            if(resourceSets.ContainsKey(culture.Name))
            {
                resourceSet =  resourceSets[culture.Name];
            }
            else
            {
                resourceSet = new DataBaseResourceSet(connectionString, culture);
                resourceSets.Add(culture.Name, resourceSet);
            }
            return resourceSet;
        }
    }
}
