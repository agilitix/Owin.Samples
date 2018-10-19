using System.Collections.Generic;
using System.Web.Http;

namespace OwinDrylocSwaggerWebAPI.Controllers
{
    public interface ITestsController
    {
    }

    public class TestsController : ApiController, ITestsController
    {
        private string _dummy;

        public TestsController()
        {
            _dummy = new string('Z', 1000000);
        }

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
