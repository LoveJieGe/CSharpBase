using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chapter13_AsyncLib
{
    public interface IImageRequest
    {
        string SearchTerm { get; set; }
        string Url { get; }
        IEnumerable<SearchItemResult> Parse(string xml);
        ICredentials Credentials { get; }
    }
}
