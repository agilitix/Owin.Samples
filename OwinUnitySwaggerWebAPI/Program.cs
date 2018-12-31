using System;
using System.Globalization;
using System.Net;
using System.Threading;
using OwinUnitySwaggerWebAPI.Injection;

namespace OwinUnitySwaggerWebAPI
{
    static class Program
    {
        static Program()
        {
            var culture_en_US = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture_en_US;
            Thread.CurrentThread.CurrentUICulture = culture_en_US;
            CultureInfo.DefaultThreadCurrentCulture = culture_en_US;
            CultureInfo.DefaultThreadCurrentUICulture = culture_en_US;
        }

        static void Main(string[] args)
        {
            string baseAddress = "http://+:5500/";
            string swaggerURL = baseAddress.Replace("+", Dns.GetHostName()) + "swagger/ui/index";

            using(IUnityProvider unity = new UnityProvider())
            using (IServer server = new Server(unity))
            {
                server.Start(baseAddress);

                Console.WriteLine();
                Console.WriteLine("Swagger URL: " + swaggerURL);

                Console.WriteLine();
                Console.Write("Press enter to exit:");
                Console.ReadLine();
            }
        }
    }
}
