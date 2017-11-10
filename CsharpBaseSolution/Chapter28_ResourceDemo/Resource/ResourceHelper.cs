using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Chapter28_ResourceDemo.Resource
{
    public static class ResourceHelper
    {
        private static Dictionary<string, ResourceManager> ResDict = new Dictionary<string, ResourceManager>();
        public static ResourceManager GetResourceManager(string baseName)
        {
            if(!ResDict.ContainsKey(baseName))
            {
                ResDict[baseName] = new ResourceManager(baseName, typeof(ResourceHelper).Assembly);
            }
            return ResDict[baseName];
        }
        public  static string GetString(string baseName,string key)
        {
            ResourceManager rm = GetResourceManager(baseName);
            return rm.GetString(key, CultureInfo.CurrentCulture);
        }
    }
}
