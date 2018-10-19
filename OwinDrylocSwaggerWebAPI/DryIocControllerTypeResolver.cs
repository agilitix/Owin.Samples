using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using DryIoc;

namespace OwinDrylocSwaggerWebAPI
{
    public class DryIocControllerTypeResolver : IHttpControllerTypeResolver
    {
        private readonly Container _container;

        public DryIocControllerTypeResolver(Container container)
        {
            _container = container;
        }

        public ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
        {
            IList<Type> registeredControllers = _container.GetServiceRegistrations()
                                                          .Where(x => IsControllerType(x.ImplementationType))
                                                          .Select(x => x.ImplementationType)
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
