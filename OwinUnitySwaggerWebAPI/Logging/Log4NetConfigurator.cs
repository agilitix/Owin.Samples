using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using log4net.Appender;
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

        public static string LogFileAppenders()
        {
            IEnumerable<FileAppender> fileAppenders = LogManager.GetRepository()
                                                                .GetAppenders()
                                                                .OfType<FileAppender>();

            string files = string.Join(", ",
                                       fileAppenders.Where(x => !string.IsNullOrWhiteSpace(x.File))
                                                    .Select(x => "\"" + x.File + "\""));
            return files;
        }
    }
}
