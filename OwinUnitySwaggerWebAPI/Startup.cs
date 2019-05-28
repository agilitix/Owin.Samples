using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Xml;
using Microsoft.Owin;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;
using Swashbuckle.Application;
using Unity.AspNet.WebApi;
using Unity;
using JsonFormatting = Newtonsoft.Json.Formatting;
using Microsoft.Owin.Cors;
using OwinUnitySwaggerWebAPI.Middlewares;
using Microsoft.Owin.Logging;
using System.Reflection;
using System.Web.Http.Controllers;
using OwinUnitySwaggerWebAPI.Injection;
using OwinUnitySwaggerWebAPI.Logging;

[assembly: OwinStartup(typeof(OwinUnitySwaggerWebAPI.Startup))]

namespace OwinUnitySwaggerWebAPI
{
    public class Startup
    {
        public static IUnityProvider Unity { get; set; }

        public void Configuration(IAppBuilder app)
        {
            // Set logger factory.
            app.SetLoggerFactory(new Log4NetLoggerFactory(Assembly.GetExecutingAssembly()));

            ILogger logger = app.CreateLogger<Startup>();
            logger.WriteInformation("App is starting, building the http configuration");

            // Create the http configuration.
            HttpConfiguration config = new HttpConfiguration();

            // Ignore SignalR related routes.
            config.Routes.IgnoreRoute("signalr", "signalr/{*pathInfo}");

            // Attribute-based routing.
            config.MapHttpAttributeRoutes();

            // Convention-based routing.
            config.Routes.MapHttpRoute(name: "DefaultApi",
                                       routeTemplate: "api/{controller}/{action}/{id}",
                                       defaults: new
                                                 {
                                                     action = RouteParameter.Optional,
                                                     id = RouteParameter.Optional,
                                                 });

            // Controllers type resolver.
            ITypeProvider<IHttpController> registeredControllers = new UnityTypeProvider<IHttpController>(Unity.Container);
            config.Services.Replace(typeof(IHttpControllerTypeResolver), new ControllerTypeResolver(registeredControllers));

            // Dependency resolver, hierarchical means one controller instance per-request.
            config.DependencyResolver = new UnityHierarchicalDependencyResolver(Unity.Container);

            // Pretty format for api messages.
            ConfigureJsonFormatter(config.Formatters.JsonFormatter);
            ConfigureXmlFormatter(config.Formatters.XmlFormatter);

            // Get Swagger parameters from unity config.
            string apiVersion = "v1";
            if (Unity.Container.IsRegistered<string>("ApiVersion"))
            {
                apiVersion = Unity.Container.Resolve<string>("ApiVersion");
            }

            // Create Swagger infos.
            string apiTitle = "API title";
            if (Unity.Container.IsRegistered<string>("ApiTitle"))
            {
                apiTitle = Unity.Container.Resolve<string>("ApiTitle");
            }

            string swaggerXmlComments = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
            if (Unity.Container.IsRegistered<string>("SwaggerXmlComments"))
            {
                // If overridden in unity config.
                swaggerXmlComments = Unity.Container.Resolve<string>("SwaggerXmlComments");
            }

            if (!File.Exists(swaggerXmlComments))
            {
                throw new FileNotFoundException("The 'XML documentation file' does not exist, please check your project property '" + Assembly.GetExecutingAssembly().GetName().Name + "/Properties/Build/XML documentation file'", swaggerXmlComments);
            }

            // Expose the API methods as Swagger.
            config.EnableSwagger(c =>
                                 {
                                     c.SingleApiVersion(apiVersion, apiTitle);
                                     c.IncludeXmlComments(swaggerXmlComments); // See project "properties / Build / XML documentation file"
                                 })
                  .EnableSwaggerUi(x => x.DisableValidator());

            // Add logging middleware.
            app.Use<LoggingMiddleware>();

            // Add other middlewares.
            ITypeProvider<OwinMiddleware> middlewares = new UnityTypeProvider<OwinMiddleware>(Unity.Container);
            foreach (Type middleware in middlewares.GetTypes())
            {
                app.Use(middleware);
            }

            // Allow cross-origin (cross-domain) resources.
            app.UseCors(CorsOptions.AllowAll);

            // We are using WebAPI.
            app.UseWebApi(config);

            // Perform final initialization of the config before it is used by incoming requests.
            config.EnsureInitialized();

            // Log end of configuration.
            logger.WriteInformation("App is started, configuration is done");
        }

        private void ConfigureJsonFormatter(JsonMediaTypeFormatter jsonFormatter)
        {
            if (jsonFormatter == null)
            {
                return;
            }

            // Converters.
            jsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter {DateTimeFormat = "o"}); // ISO8601="2018-11-20T08:55:05.3890640+02:00"
            jsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter()); // Enum as string.

            // Settings.
            jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            jsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.All;
            jsonFormatter.SerializerSettings.Formatting = JsonFormatting.Indented;
        }

        private void ConfigureXmlFormatter(XmlMediaTypeFormatter xmlFormatter)
        {
            if (xmlFormatter == null)
            {
                return;
            }

            xmlFormatter.WriterSettings.Indent = true;
            xmlFormatter.WriterSettings.OmitXmlDeclaration = false;
            xmlFormatter.WriterSettings.NamespaceHandling = NamespaceHandling.OmitDuplicates;
            xmlFormatter.WriterSettings.Encoding = Encoding.UTF8;
        }
    }
}
