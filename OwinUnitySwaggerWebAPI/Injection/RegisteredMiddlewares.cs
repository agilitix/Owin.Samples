using System;
using System.Collections.Generic;
using Microsoft.Owin;
using Unity;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public class RegisteredMiddlewares : IRegisteredMiddlewares
    {
        protected readonly IRegisteredTypesSelector<OwinMiddleware> _selector;

        public RegisteredMiddlewares(IUnityContainer container)
        {
            _selector = new RegisteredTypesSelector<OwinMiddleware>(container);
        }

        public IEnumerable<Type> GetMiddlewares()
        {
            return _selector.GetTypes();
        }
    }
}
