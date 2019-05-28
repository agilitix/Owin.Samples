using System.Web.Http;
using OwinUnitySwaggerWebAPI.Common.Services;
using Unity;

namespace OwinUnitySwaggerWebAPI.Common.Controllers
{
    public class ApiControllerBase : ApiController
    {
        [OptionalDependency]
        protected IRegisteredServices Services { get; set; }
    }
}
