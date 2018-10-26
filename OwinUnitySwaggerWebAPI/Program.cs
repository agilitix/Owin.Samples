using System;

namespace OwinUnitySwaggerWebAPI
{
    static class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:5000/";
            string swaggerURL = baseAddress + "swagger/ui/index";

            using (Server server = new Server())
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
