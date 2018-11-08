using System.IO;
using Microsoft.Owin.Logging;
using System.Reflection;
using System.Threading;
using log4net.Config;

namespace OwinUnitySwaggerWebAPI.Logging
{
    internal class Log4NetLoggerFactory : ILoggerFactory
    {
        private readonly Assembly _repositoryAssembly;

        public Log4NetLoggerFactory(Assembly repositoryAssembly)
        {
            _repositoryAssembly = repositoryAssembly;
        }

        public ILogger Create(string name)
        {
            return new Log4NetLogger(_repositoryAssembly, name);
        }
    }
}
