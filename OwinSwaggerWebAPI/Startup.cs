using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(OwinSwaggerWebAPI.Startup))]

namespace OwinSwaggerWebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(name: "DefaultApi",
                                       routeTemplate: "api/{controller}/{id}",
                                       defaults: new
                                                 {
                                                     id = RouteParameter.Optional
                                                 });
            app.UseWebApi(config);

            config.EnableSwagger(c =>
                                 {
                                     c.SingleApiVersion("v1", "My first API");
                                     c.IncludeXmlComments("OwinSwaggerWebAPI.XML");
                                 })
                  .EnableSwaggerUi(x => x.DisableValidator());
        }
    }
}
