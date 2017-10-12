using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chapter13_AsyncLib
{
    public class BingRequest:IImageRequest
    {
        private const string AppId = "请输入你的必应的AppID";
        public BingRequest()
        {
            Count = 50;
            OffSet = 0;
        }
        public int Count
        { get; set; }
        public int OffSet { get; set; }
        private string searchTerm;
        public string SearchTerm
        {
            get { return searchTerm; }
            set { this.searchTerm = value; }
        }

        public ICredentials Credentials
        {
            get { return new NetworkCredential(AppId, AppId); }
        }
        public string Url
        {
            get
            {
                return string.Format("https://api.datamarket.azure.com/" +
                    "Data.ashx/Bing/Search/v1/Image?Query=%27{0}%27&" +
                    "$top={1}&$skip={2}&$format=Atom", SearchTerm, Count, OffSet);
            }
        }

        public IEnumerable<SearchItemResult> Parse(string xml)
        {
            XElement respXml = XElement.Parse(xml) ;
            XNamespace d = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices");
            XNamespace m = XNamespace.Get("http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
            var result = from item in respXml.Descendants(m + "properties")
                         select new SearchItemResult()
                         {
                             Title = new string(item.Element(d+"Title").Value.Take(50).ToArray()),
                             Url = item.Element(d+"MediaUrl").Value,
                             ThumbnailUrl = item.Element(d+"Thumbnail").Element(d+"MediaUrl").Value,
                             Source="Bing"
                         };
            return result;
        }
    }
}
