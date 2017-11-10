using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter28_ResourceDemo.Resource
{
    public class R
    {
        private static IResourceFactory factory;
        static R()
        {
            factory = new ResourceFactory();
        }
        public static string G(string message)
        {
            if(!string.IsNullOrEmpty(message))
            {
                int index = message.IndexOf(":");
                if(index!=-1)
                {
                    string key = message.Split(':')[0];
                    string value = message.Split(':')[1];
                    return factory.GetResource(key, value);
                }
            }
            return message;
        }
    }
}
