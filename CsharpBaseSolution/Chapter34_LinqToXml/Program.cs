using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chapter34_LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadInternelXml();
            Console.Read();
        }
        static void ReadInternelXml()
        {
            XDocument doc = XDocument.Load(@"http://geekswithblogs.net/evjen/Rss.aspx");
            var query = from rss in doc.Descendants("channel")
                        select new
                        {
                            Title = rss.Element("title").Value,
                            Description = rss.Element("description").Value,
                            Link = rss.Element("link").Value
                        };
            foreach(var item in query)
            {
                Console.WriteLine("Title:" + item.Title + "\r\nDescription:"
                    + item.Description + "\r\nLink:" + item.Link);
            }
            Console.WriteLine("----------------------------");
            var queryPosts = from myPosts in doc.Descendants("item")
                             select new
                             {
                                 Title = myPosts.Element("title").Value,
                                 Category = myPosts.Element("category").Value,
                                 Description = myPosts.Element("description").Value,
                                 PubDate = myPosts.Element("pubDate").Value,
                                 Comments = myPosts.Element("comments").Value
                             };
            foreach (var item in queryPosts)
            {
                Console.WriteLine("Title:" + item.Title + "\r\nDescription:"
                                  + item.Description + "\r\nCategory:" + item.Category
                                  +"\r\nPubDate:"+item.PubDate+"\r\nComments:"+item.Comments);
            }
        }
    }
}
