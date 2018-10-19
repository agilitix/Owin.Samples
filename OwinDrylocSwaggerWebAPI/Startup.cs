using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Xml;
using DryIoc;
using DryIoc.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;
using OwinDrylocSwaggerWebAPI;
using OwinDrylocSwaggerWebAPI.Controllers;
using Swashbuckle.Application;
using Formatting = Newtonsoft.Json.Formatting;

[assembly: OwinStartup(typeof(Startup))]

namespace OwinDrylocSwaggerWebAPI
{
    public class Startup
    {
        private static IDisposable _webApp;

        public static void Start(string url)
        {
            _webApp = WebApp.Start<Startup>(url);
        }

        public static void Stop()
        {
            _webApp.Dispose();
        }

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

            Container di = new Container();

            // NOTE: Registers ISession provider to work with injected Request
            di.Register(Made.Of(() => GetSession(Arg.Of<HttpRequestMessage>())));

            di.Register<IHttpController, ValuesController>(new CurrentScopeReuse());
            di.Register<IHttpController, TestsController>(new CurrentScopeReuse());

            // Controller resolver.
            config.Services.Replace(typeof(IHttpControllerTypeResolver), new DryIocControllerTypeResolver(di));

            // Dependency resolver.
            config.DependencyResolver = new DryIocDependencyResolver(di);

            // Formatters.
            ConfigureJsonFormatter(config.Formatters.JsonFormatter);
            ConfigureXmlFormatter(config.Formatters.XmlFormatter);

            config.EnableSwagger(c =>
                                 {
                                     c.SingleApiVersion("v1", "My first API");
                                     c.IncludeXmlComments("OwinDryIocSwaggerWebAPI.xml");
                                 })
                  .EnableSwaggerUi(x => x.DisableValidator());

            di.WithWebApi(config);
            app.UseWebApi(config);
        }

        public static ISession GetSession(HttpRequestMessage request)
        {
            // TODO: This is just a sample. Insert whatever session management logic you need.
            Session session = new Session();
            return session;
        }

        private void ConfigureJsonFormatter(JsonMediaTypeFormatter jsonFormatter)
        {
            if (jsonFormatter == null)
            {
                return;
            }

            // Converters.
            jsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter {DateTimeFormat = "o"}); // ISO8601
            jsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter()); // Enum as string.

            // Settings.
            jsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            jsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.All;
            jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
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

    public interface ISession
    {
        string Token { get; }
    }

    public class Session : ISession
    {
        string m_token;

        public Session()
        {
            Console.WriteLine("Session()");
        }

        public string Token => m_token ?? (m_token = Guid.NewGuid().ToString());
    }

    public class RootController : ApiController
    {
        readonly ISession m_session;

        public RootController(ISession session_)
        {
            m_session = session_;
        }

        [Route()]
        public IHttpActionResult GetApiRoot()
        {
            return Json(new
                        {
                            type = "root",
                            token = m_session.Token
                        });
        }
    }
}
