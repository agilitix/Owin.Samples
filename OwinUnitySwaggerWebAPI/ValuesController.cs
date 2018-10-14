using System.Collections.Generic;
using System.Web.Http;

namespace OwinUnitySwaggerWebAPI
{
    public class ValuesController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new List<string>
                   {
                       "val1",
                       "val2",
                   };
        }

        [HttpGet]
        public string Get(int id)
        {
            return "val" + id;
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
