using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;
using Microsoft.Owin;
using Owin;
using Swashbuckle.Application;
using Unity;
using Unity.AspNet.WebApi;

[assembly: OwinStartup(typeof(OwinUnitySwaggerWebAPI.Startup))]

namespace OwinUnitySwaggerWebAPI
{
    /// <summary>
    /// The startup class.
    /// </summary>
    public class Startup
    {
        private IUnityContainer _container;

        /// <summary>
        /// Configure at startup.
        /// </summary>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Build the IoC container.
            UnityProvider unityProvider = new UnityProvider("unity.startup.config");
            _container = unityProvider.Unity;

            RegisterDependencyResolver(config);
            RegisterFiltersProvider(config);

            config.Services.Replace(typeof(IHttpControllerTypeResolver), new ControllerTypeResolver(_container));

            config.Routes.MapHttpRoute(name: "DefaultApi",
                                       routeTemplate: "api/{controller}/{id}",
                                       defaults: new
                                       {
                                           id = RouteParameter.Optional
                                       });

            // Routes are decorated in classes with attributes.
            //config.MapHttpAttributeRoutes();

            config.EnableSwagger(c =>
                                 {
                                     c.SingleApiVersion("v1", "My first API");
                                     c.IncludeXmlComments("OwinSwaggerWebAPI.xml");
                                 })
                  .EnableSwaggerUi(x => x.DisableValidator());

            app.UseWebApi(config);
        }

        private void RegisterDependencyResolver(HttpConfiguration config)
        {
            IDependencyResolver dependencies = new UnityDependencyResolver(_container);
            //config.Services.Replace(typeof(IDependencyResolver), dependencies);
            config.DependencyResolver = dependencies;
        }

        private void RegisterFiltersProvider(HttpConfiguration config)
        {
            IList<IFilterProvider> providers = config.Services.GetFilterProviders().ToList();
            config.Services.Add(typeof(IFilterProvider), new UnityActionDescriptorFilterProvider(_container));
            IFilterProvider defaultprovider = providers.First(p => p is ActionDescriptorFilterProvider);
            config.Services.Remove(typeof(IFilterProvider), defaultprovider);
        }
    }
}
