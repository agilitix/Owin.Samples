using System;
using System.Collections.Generic;
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
using OwinUnitySwaggerWebAPI.Logging;

[assembly: OwinStartup(typeof(OwinUnitySwaggerWebAPI.Startup))]

namespace OwinUnitySwaggerWebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Set logger factory and log welcome message.
            app.SetLoggerFactory(new Log4NetLoggerFactory(Assembly.GetExecutingAssembly()));
            ILogger logger = app.CreateLogger<Startup>();
            logger.WriteInformation("App is starting, building configuration");

            // Create configuration.
            HttpConfiguration config = new HttpConfiguration();

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
            config.Services.Replace(typeof(IHttpControllerTypeResolver), new ControllerTypeResolver(UnityConfig.Container));

            // Dependency resolver, hierarchical means one controller instance per-request.
            config.DependencyResolver = new UnityHierarchicalDependencyResolver(UnityConfig.Container);

            // Pretty format for output.
            ConfigureJsonFormatter(config.Formatters.JsonFormatter);
            ConfigureXmlFormatter(config.Formatters.XmlFormatter);

            // Some parameters from unity config.
            string apiVersion = UnityConfig.Container.Resolve<string>("ApiVersion");
            string apiTitle = UnityConfig.Container.Resolve<string>("ApiTitle");
            string swaggerXmlComments = UnityConfig.Container.Resolve<string>("SwaggerXmlComments");

            // Expose the API methods as Swagger.
            config.EnableSwagger(c =>
                                 {
                                     c.SingleApiVersion(apiVersion, apiTitle);
                                     c.IncludeXmlComments(swaggerXmlComments); // See project properties => Build / XML documentation file
                                 })
                  .EnableSwaggerUi(x => x.DisableValidator());

            // Add logging middleware.
            app.Use<LoggingMiddleware>();

            // Allow cross-origin (cross-domain) resources.
            app.UseCors(CorsOptions.AllowAll);

            // We are using WebAPI.
            app.UseWebApi(config);

            // Perform final initialization of the config before it is used to process requests.
            config.EnsureInitialized();

            // Log end of configuration.
            logger.WriteInformation("App is started, configuration is built");
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
