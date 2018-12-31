using System.IO;
using log4net.Config;

namespace OwinUnitySwaggerWebAPI.Logging
{
    public static class Log4NetConfigurator
    {
        public static void Configure(string log4netConfig = "log4net.config")
        {
            if (File.Exists(log4netConfig))
            {
                // Custom log4net config file.
                XmlConfigurator.Configure(new FileInfo(log4netConfig));
            }
            else
            {
                // Read log4net config from App.config file.
                XmlConfigurator.Configure();
            }
        }
    }
}
