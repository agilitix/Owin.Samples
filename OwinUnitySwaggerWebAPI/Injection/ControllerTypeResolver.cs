﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Unity;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public class ControllerTypeResolver : IHttpControllerTypeResolver
    {
        private readonly IRegisteredControllers _registeredControllers;

        public ControllerTypeResolver(IRegisteredControllers registeredControllers)
        {
            _registeredControllers = registeredControllers;
        }

        public ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
        {
            return _registeredControllers.GetControllers() as ICollection<Type>;
        }
    }
}