using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public class ControllerTypeResolver : IHttpControllerTypeResolver
    {
        private readonly ITypeProvider<IHttpController> _registeredControllers;

        public ControllerTypeResolver(ITypeProvider<IHttpController> registeredControllers)
        {
            _registeredControllers = registeredControllers;
        }

        public ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
        {
            return _registeredControllers.GetTypes() as ICollection<Type>;
        }
    }
}
