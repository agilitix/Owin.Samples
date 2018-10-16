using System;

namespace OwinUnitySwaggerWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:5000/";
            // Swagger URL : "http://localhost:5000/swagger/ui/index"

            Startup.Start(baseAddress);

            Console.Write("Hit enter to exit:");
            Console.ReadLine();

            Startup.Stop();
        }
    }
}
