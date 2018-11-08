using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;

namespace OwinUnitySwaggerWebAPI.Logging
{
    public class Log4NetConfigurator
    {
        public static void Configure(string log4netConfig = "log4net.config")
        {
            if (File.Exists(log4netConfig))
            {
                // Custom config file.
                XmlConfigurator.Configure(new FileInfo(log4netConfig));
            }
            else
            {
                // Config is read from App.config file.
                XmlConfigurator.Configure();
            }
        }
    }
}
