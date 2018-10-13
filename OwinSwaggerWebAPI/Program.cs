using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace OwinSwaggerWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:5000/";

            using (WebApp.Start<Startup>(baseAddress))
            {
                HttpClient client = new HttpClient();

                var response = client.GetAsync(baseAddress + "api/values").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                Console.ReadLine();
            }
        }
    }
}
