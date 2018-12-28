using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using OwinUnitySwaggerWebAPI.Injection;

namespace OwinUnitySwaggerWebAPI
{
    static class Program
    {
        static Program()
        {
            var culture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentCulture.GetType().GetProperty("DefaultThreadCurrentCulture")?.SetValue(Thread.CurrentThread.CurrentCulture, culture, null);
            //Thread.CurrentThread.CurrentCulture.GetType().GetProperty("DefaultThreadCurrentUICulture")?.SetValue(Thread.CurrentThread.CurrentCulture, culture, null);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:5500/";
            string swaggerURL = baseAddress + "swagger/ui/index";

            using(IUnityProvider unity = new UnityProvider())
            using (IServer server = new Server(unity))
            {
                server.Start(baseAddress);

                Console.WriteLine();
                Console.WriteLine("Swagger URL=" + swaggerURL);

                Console.WriteLine();
                Console.Write("Press enter to exit:");
                Console.ReadLine();
            }
        }
    }
}
