using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Unity;

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
            IList<Type> registeredControllers = _container.Registrations
                                                          .Where(x => IsControllerType(x.MappedToType))
                                                          .Select(x => x.MappedToType)
                                                          .ToList();
            return registeredControllers;
        }

        protected bool IsControllerType(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsClass
                   && type.IsVisible
                   && !type.IsAbstract
                   && typeof(IHttpController).IsAssignableFrom(type);
        }
    }
}
