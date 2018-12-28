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
            // Log request.
            _logger.Info("Request: " + new RequestInfoLog(context.Request));

            await Next.Invoke(context);

            // Log response.
            _logger.Info("Response: " + new ResponseInfoLog(context.Response));
        }
    }
}
