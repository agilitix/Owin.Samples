using log4net.Config;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;

namespace OwinUnitySwaggerWebAPI.Logging
{
    internal class Log4NetLogger : Microsoft.Owin.Logging.ILogger
    {
        private readonly Logger _logger;

        public Log4NetLogger(Assembly repositoryAssembly, string name)
        {
            _logger = (Logger) LoggerManager.GetLogger(repositoryAssembly, name);
        }

        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            Level level = MapEventTypeToLevel(eventType);

            bool isEnabled = _logger.IsEnabledFor(level);
            if (state == null)
            {
                // When calling WriteCore with only TraceEventType to check the IsEnabled level (no log event will be written).
                return isEnabled;
            }

            if (!isEnabled)
            {
                return false;
            }

            if (!HandleFilteredExceptions(level, exception))
            {
                _logger.Log(level, formatter(state, exception), exception);
            }

            return true;
        }

        private bool HandleFilteredExceptions(Level level, Exception exception)
        {
            ObjectDisposedException objectDisposedException;
            if (level == Level.Error
                && (objectDisposedException = exception as ObjectDisposedException) != null
                && objectDisposedException.ObjectName == typeof(HttpListener).FullName)
            {
                _logger.Log(Level.Warn, "Warning: Exception=" + exception.GetType().FullName + ", ObjectName=" + objectDisposedException.ObjectName, null);
                return true;
            }

            return false;
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
