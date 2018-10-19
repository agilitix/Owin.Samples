using System.Collections.Generic;
using System.Web.Http;

namespace OwinDrylocSwaggerWebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        private string _dummy;

        public ValuesController()
        {
            _dummy = new string('Y', 1000000);
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
