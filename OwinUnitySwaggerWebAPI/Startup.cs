using System;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Xml;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;
using Swashbuckle.Application;
using Unity.AspNet.WebApi;
using JsonFormatting = Newtonsoft.Json.Formatting;

[assembly: OwinStartup(typeof(OwinUnitySwaggerWebAPI.Startup))]

namespace OwinUnitySwaggerWebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Attribute-based routing.
            config.MapHttpAttributeRoutes();

            // Convention-based routes.
            config.Routes.MapHttpRoute(name: "DefaultApi",
                                       routeTemplate: "api/{controller}/{action}/{id}",
                                       defaults: new
                                                 {
                                                     action = RouteParameter.Optional,
                                                     id = RouteParameter.Optional,
                                                 });

            // Controllers type resolver.
            config.Services.Replace(typeof(IHttpControllerTypeResolver), new ControllerTypeResolver(UnityConfig.GetContainer()));

            // Dependency resolver, hierarchical means one controller instance per-request.
            config.DependencyResolver = new UnityHierarchicalDependencyResolver(UnityConfig.GetContainer());

            // Pretty format for output.
            ConfigureJsonFormatter(config.Formatters.JsonFormatter);
            ConfigureXmlFormatter(config.Formatters.XmlFormatter);

            // Expose API contracts in Swagger.
            config.EnableSwagger(c =>
                                 {
                                     c.SingleApiVersion("v1", "My first API");
                                     c.IncludeXmlComments("OwinUnitySwaggerWebAPI.xml");
                                 })
                  .EnableSwaggerUi(x => x.DisableValidator());

            // We are using WebAPI.
            app.UseWebApi(config);
        }

        private void ConfigureJsonFormatter(JsonMediaTypeFormatter jsonFormatter)
        {
            if (jsonFormatter == null)
            {
                return;
            }

            // Converters.
            jsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter {DateTimeFormat = "o"}); // ISO8601="2018-10-20T08:55:05.3890640+02:00"
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
