using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Unity;
using Unity.Registration;

namespace OwinUnitySwaggerWebAPI
{
    public class ControllerTypeResolver : IHttpControllerTypeResolver
    {
        private readonly IUnityContainer _container;

        public ControllerTypeResolver(IUnityContainer container)
        {
            _container = container;
        }

        public ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
        {
            IList<Type> unityControllers = _container.Registrations.Where(x => typeof(IHttpController).IsAssignableFrom(x.MappedToType))
                                                     .Select(x => x.MappedToType)
                                                     .ToList();
            return unityControllers;
        }
    }
}
