using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using OwinUnitySwaggerWebAPI.Common.Initialization;
using OwinUnitySwaggerWebAPI.Injection;
using Unity;

namespace OwinUnitySwaggerWebAPI.Initialization
{
    public class ControllerInitializer : IControllerInitializer
    {
        protected readonly IUnityContainer _container;
        protected readonly ITypeProvider<IHttpController> _controllersProvider;

        public ControllerInitializer(IUnityContainer container, ITypeProvider<IHttpController> controllersProvider)
        {
            _container = container;
            _controllersProvider = controllersProvider;
        }

        public void OneTimeStartup()
        {
            Invoke<OneTimeStartupAttribute>();
        }

        public void OneTimeShutdown()
        {
            Invoke<OneTimeShutdownAttribute>();
        }

        private void Invoke<T>() where T : Attribute
        {
            IEnumerable<MethodInfo> methods = _controllersProvider.GetTypes()
                                                                  .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public)
                                                                                    .Where(x => x.GetCustomAttributes()
                                                                                                 .OfType<T>()
                                                                                                 .Any()));
            foreach (MethodInfo method in methods)
            {
                object[] parameters = ResolveParameters(method);
                method.Invoke(null, parameters);
            }
        }

        private object[] ResolveParameters(MethodInfo method)
        {
            IEnumerable<object> parameters = method.GetParameters()
                                                   .Select(ResolveParam);
            return parameters.ToArray();
        }

        private object ResolveParam(ParameterInfo paramInfo)
        {
            if (_container.IsRegistered(paramInfo.ParameterType, paramInfo.Name))
            {
                return _container.Resolve(paramInfo.ParameterType, paramInfo.Name);
            }

            if (_container.IsRegistered(paramInfo.ParameterType))
            {
                return _container.Resolve(paramInfo.ParameterType);
            }

            return paramInfo.ParameterType.IsValueType
                       ? Activator.CreateInstance(paramInfo.ParameterType)
                       : null;
        }
    }
}
