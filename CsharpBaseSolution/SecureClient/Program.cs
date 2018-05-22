using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SecureClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //NotAuthenticated().Wait();
            RegisterUser().Wait();
            Console.Read();
        }
        private static async Task NotAuthenticated()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6303");
            HttpResponseMessage response = await client.GetAsync("api/values");
            Console.WriteLine(response.StatusCode);
            string result =await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
        private static async Task RegisterUser()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:6303");
            string requestUri = "/api/Account/Register";
            var user = new
            {
                Email = "christian",
                Password = "Password-123",
                ConfirmPassword = "Password-123"
            };
            HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, user);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("注册成功");
        }

    }
}
