using System.Collections.Generic;
using System.Web.Http;

namespace OwinUnitySwaggerWebAPI.Controllers
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
