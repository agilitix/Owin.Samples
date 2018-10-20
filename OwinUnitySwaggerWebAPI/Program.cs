using System;

namespace OwinUnitySwaggerWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:5000/";
            // Swagger URL : "http://localhost:5000/swagger/ui/index"

            Server server = new Server();
            server.Setup("unity.startup.config");

            server.Start(baseAddress);

            Console.Write("Press enter to exit:");
            Console.ReadLine();

            server.Stop();
        }
    }
}
