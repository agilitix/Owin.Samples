using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OwinUnitySwaggerWebAPI.Client
{
    static class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:5000/";

            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < 1000000; ++i)
                {
                    var r1 = client.GetAsync(baseAddress + "api/Values/Get").Result;
                    Console.WriteLine(r1.Content.ReadAsStringAsync().Result);

                    var r2 = client.GetAsync(baseAddress + "api/Tests/Get").Result;
                    Console.WriteLine(r2.Content.ReadAsStringAsync().Result);
                }
            }
        }
    }
}
