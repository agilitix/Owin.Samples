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
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
        }

        static void Main(string[] args)
        {
            // Add/delete user acl for the url and port:
            // netsh http add urlacl url=http://localhost:5500/ user=DOMAIN\user
            // netsh http delete urlacl url=http://localhost:5500/

            string baseAddress = "http://localhost:5500/";
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
