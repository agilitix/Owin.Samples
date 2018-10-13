using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;

namespace OwinSwaggerWebAPI
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
