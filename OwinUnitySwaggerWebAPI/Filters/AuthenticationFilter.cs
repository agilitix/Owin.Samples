using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace OwinUnitySwaggerWebAPI.Filters
{
    internal class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null
                || string.IsNullOrWhiteSpace(actionContext.Request.Headers.Authorization.Parameter))
            {
                //actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                //string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                //string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
            }
        }
    }
}
