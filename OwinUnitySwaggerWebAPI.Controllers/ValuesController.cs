using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using OwinUnitySwaggerWebAPI.Common.Initialization;

namespace OwinUnitySwaggerWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        [OneTimeStartup]
        public static void Startup()
        {
        }

        [OneTimeShutdown]
        public static void Shutdown()
        {
        }

        public IEnumerable<string> Get()
        {
            return new List<string>
                   {
                       "val1",
                       "val2",
                   };
        }

        public string Get(int id)
        {
            return "val" + id;
        }

        public void Post([FromBody] string value)
        {
        }

        public void Put(int id, [FromBody] string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
