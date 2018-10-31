using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace OwinUnitySwaggerWebAPI.Logging
{
    internal class Log4NetLogger : Microsoft.Owin.Logging.ILogger
    {
        private readonly Logger _logger;

        static Log4NetLogger()
        {
            Configure();
        }

        public static void Configure(string log4netConfig = "log4net.config")
        {
            if(File.Exists(log4netConfig))
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

        public Log4NetLogger(Assembly repositoryAssembly, string name)
        {
            _logger = (Logger)LoggerManager.GetLogger(repositoryAssembly, name);
        }

        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            Level level = MapEventTypeToLevel(eventType);

            bool isEnabled = _logger.IsEnabledFor(level);
            if (state == null)
            {
                // Calling WriteCore with only TraceEventType to check the IsEnabled level, no log event will be written.
                return isEnabled;
            }
            else if (!isEnabled)
            {
                return false;
            }

            _logger.Log(level, formatter(state, exception), exception);
            return true;
        }

        static Level MapEventTypeToLevel(TraceEventType eventType)
        {
            switch (eventType)
            {
                case TraceEventType.Critical:
                    return Level.Fatal;
                case TraceEventType.Error:
                    return Level.Error;
                case TraceEventType.Warning:
                    return Level.Warn;
                case TraceEventType.Information:
                    return Level.Info;
                case TraceEventType.Verbose:
                    return Level.Trace;
                case TraceEventType.Start:
                    return Level.Debug;
                case TraceEventType.Stop:
                    return Level.Debug;
                case TraceEventType.Suspend:
                    return Level.Debug;
                case TraceEventType.Resume:
                    return Level.Debug;
                case TraceEventType.Transfer:
                    return Level.Debug;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventType));
            }
        }
    }
}
