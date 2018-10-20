using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OwinUnitySwaggerWebAPI.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < 1000000; ++i)
                {
                    var r1 = client.GetAsync("http://localhost:5000/api/Values/Get").Result;
                    Console.WriteLine(r1.Content.ReadAsStringAsync().Result);

                    var r2 = client.GetAsync("http://localhost:5000/api/Tests/Get").Result;
                    Console.WriteLine(r2.Content.ReadAsStringAsync().Result);
                }
            }
        }
    }
}
