using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;
using Microsoft.Owin;
using Owin;
using Swashbuckle.Application;
using Unity;

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

            RegisterAssembliesResolver(config);
            RegisterFiltersProvider(config);

            //config.Routes.MapHttpRoute(name: "DefaultApi",
            //                           routeTemplate: "api/{controller}/{id}",
            //                           defaults: new
            //                                     {
            //                                         id = RouteParameter.Optional
            //                                     });

            // Routes are decorated in classes with attributes.
            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);

            config.EnableSwagger(c =>
                                 {
                                     c.SingleApiVersion("v1", "My first API");
                                     c.IncludeXmlComments("OwinSwaggerWebAPI.XML");
                                 })
                  .EnableSwaggerUi(x => x.DisableValidator());
        }

        private void RegisterAssembliesResolver(HttpConfiguration config)
        {
            DependencyResolver dependencies = new DependencyResolver(_container);
            config.Services.Replace(typeof(IAssembliesResolver), dependencies);
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
