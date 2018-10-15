using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace OwinUnitySwaggerWebAPI
{
    public interface ITestsController
    {
    }

    public class TestsController : ApiController, ITestsController
    {
        public IEnumerable<string> Get()
        {
            return new List<string>
                   {
                       "test1",
                       "test2",
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
