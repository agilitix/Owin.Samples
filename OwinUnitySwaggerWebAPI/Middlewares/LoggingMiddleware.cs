using log4net;
using Microsoft.Owin;
using System.Reflection;
using System.Threading.Tasks;

namespace OwinUnitySwaggerWebAPI.Middlewares
{
    internal class LoggingMiddleware : OwinMiddleware
    {
        protected static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public LoggingMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            // Log before request is processed.
            _logger.Info("Received request blabla");

            await Next.Invoke(context);

            // Log after request has been processed.
        }
    }
}
