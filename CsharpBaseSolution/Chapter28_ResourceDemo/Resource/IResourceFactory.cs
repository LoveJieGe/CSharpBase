using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter28_ResourceDemo.Resource
{
    public interface IResourceFactory
    {
        string GetResource(string key, string orginal);
    }
}
