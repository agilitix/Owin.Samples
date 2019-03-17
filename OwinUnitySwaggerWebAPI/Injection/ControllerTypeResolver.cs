using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public class ControllerTypeResolver : IHttpControllerTypeResolver
    {
        private readonly ITypeProvider<IHttpController> _controllersProvider;

        public ControllerTypeResolver(ITypeProvider<IHttpController> controllersProvider)
        {
            _controllersProvider = controllersProvider;
        }

        public ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
        {
            return _controllersProvider.GetTypes() as ICollection<Type>;
        }
    }
}
