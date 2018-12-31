using System;
using System.Collections.Generic;
using System.Web.Http;
using OwinUnitySwaggerWebAPI.Common.Controllers;
using OwinUnitySwaggerWebAPI.Common.Initialization;
using OwinUnitySwaggerWebAPI.Common.Services;

namespace OwinUnitySwaggerWebAPI.Controllers
{
    public class ValuesController : ApiControllerBase
    {
        [OneTimeStartup]
        public static void Startup(IRegisteredServices services)
        {
        }

        [OneTimeShutdown]
        public static void Shutdown()
        {
        }

        [HttpGet]
        [Route("api/Values/get_vals")]
        public IEnumerable<string> Get()
        {
            string apiTitle = Services?.Resolve<string>("ApiTitle");

            return new List<string>
                   {
                       "val1",
                       "val2",
                       apiTitle,
                       DateTime.Now.ToString("o"),
                   };
        }

        [HttpGet]
        public string Get(int id)
        {
            return "val" + id;
        }
    }
}
