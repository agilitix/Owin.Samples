using System;
using System.Collections;
using System.Collections.Generic;
using OwinUnitySwaggerWebAPI.Injection;

namespace OwinUnitySwaggerWebAPI
{
    static class Program
    {
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
