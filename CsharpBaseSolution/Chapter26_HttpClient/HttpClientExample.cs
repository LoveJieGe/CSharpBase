using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Chapter26_HttpClient
{
    public class HttpClientExample
    {
        public static  async void GetData()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = null;
            responseMessage =  await client.GetAsync("http://services.odata.org./Northwind/Northwind.svc/Regions");
            if(responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("响应状态吗:" + responseMessage.StatusCode + "--" + responseMessage.ReasonPhrase);
                string responseBidyAsText = responseMessage.Content.ReadAsStringAsync().Result;
                Console.WriteLine("接收到的有效负荷：" + responseBidyAsText.Length + "字符");
            }
        }

        public void ProcessBrowser()
        {
           ;
        }
    }
}
