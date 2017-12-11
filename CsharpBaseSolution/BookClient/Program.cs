using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace BookClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("client app ,wait from service");
            Console.ReadLine();
            //ReadArraySample().Wait();
            //ReadXmlSample().Wait();
            ReadWithExtensionSample().Wait();
            Console.ReadLine();
        }
        private static async Task ReadArraySample()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:36805");
            string response = await client.GetStringAsync("/api/BookChapters");
            Console.WriteLine(response);
            var serializer = new JavaScriptSerializer();
            BookChapter[] result =  serializer.Deserialize<BookChapter[]>(response);
            foreach (BookChapter chapter in result)
            {
                Console.WriteLine(chapter.Title);
            }
        }

        private static async Task ReadXmlSample()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:36805");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
            string response = await client.GetStringAsync("/api/BookChapters/3");
            Console.WriteLine(response);
            XDocument doc = new XDocument();
            XElement chapter = XElement.Parse(response);
            Console.WriteLine("{0}", chapter);
        }
        private static async Task ReadWithExtensionSample()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:36805");
            HttpResponseMessage response = await client.GetAsync("/api/BookChapters/3");
            BookChapter chapter =  await response.Content.ReadAsAsync<BookChapter>();
            Console.WriteLine("Number:{0},Title:{1}", chapter.Number, chapter.Title);
        }
    }
}
