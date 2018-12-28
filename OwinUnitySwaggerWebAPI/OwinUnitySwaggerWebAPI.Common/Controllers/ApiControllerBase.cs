using System.Web.Http;
using OwinUnitySwaggerWebAPI.Common.Services;
using Unity.Attributes;

namespace OwinUnitySwaggerWebAPI.Common.Controllers
{
    public class ApiControllerBase : ApiController
    {
        [OptionalDependency]
        protected IRegisteredServices Services { get; set; }
    }
}
