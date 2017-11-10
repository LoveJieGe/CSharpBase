using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter28_ResourceDemo.Resource
{
    public class ResourceFactory : IResourceFactory
    {
        private List<string> resxList;
        public ResourceFactory()
        {
            resxList = new List<string>();
            resxList.Add("Chapter28_ResourceDemo.Resources.Admins");
            resxList.Add("Chapter28_ResourceDemo.Resources.Demo");
        }
        public string GetResource(string key, string orginal)
        {
            for(int i = 0;i<resxList.Count;i++)
            {
                string resource = ResourceHelper.GetString(resxList[i], key);
                if (!string.IsNullOrEmpty(resource))
                    return resource;
            }
            return orginal;
        }
    }
}
