using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace OwinUnitySwaggerWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:5000/";

            using (WebApp.Start<Startup>(baseAddress))
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage responseTests = client.GetAsync(baseAddress + "api/Tests").Result;
                Console.WriteLine(responseTests);
                Console.WriteLine(responseTests.Content.ReadAsStringAsync().Result);

                HttpResponseMessage responseValues = client.GetAsync(baseAddress + "api/Values").Result;
                Console.WriteLine(responseValues);
                Console.WriteLine(responseValues.Content.ReadAsStringAsync().Result);

                Console.ReadLine();
            }
        }
    }
}
