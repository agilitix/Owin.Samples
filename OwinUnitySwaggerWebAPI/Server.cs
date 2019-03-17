using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using log4net;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Logging;
using OwinUnitySwaggerWebAPI.Initialization;
using OwinUnitySwaggerWebAPI.Injection;
using OwinUnitySwaggerWebAPI.Logging;

namespace OwinUnitySwaggerWebAPI
{
    public class Server : IServer
    {
        protected IDisposable _webApp;
        protected IControllerInitializer _initializer;

        public Server(IUnityProvider unityProvider)
        {
            Startup.Unity = unityProvider;

            ITypeProvider<IHttpController> registeredControllers = new UnityTypeProvider<IHttpController>(unityProvider.Container);
            _initializer = new ControllerInitializer(unityProvider.Container, registeredControllers);
        }

        public void Start(string baseUrl)
        {
            Log4NetConfigurator.Configure();

            Welcome();

            _initializer.OneTimeStartup();

            // If you got "access denied" exception, run this app in elevated mode or allow the tcp port for the app.
            _webApp = WebApp.Start<Startup>(baseUrl);
        }

        public void Dispose()
        {
            IControllerInitializer init = Interlocked.Exchange(ref _initializer, null);
            init?.OneTimeShutdown();

            IDisposable webApp = Interlocked.Exchange(ref _webApp, null);
            webApp?.Dispose();

            Tears();
        }

        private static void Welcome()
        {
            ILogger logger = new Log4NetLoggerFactory(Assembly.GetExecutingAssembly()).Create("welcome");

            logger.WriteInformation("---------------------------------------------------------------------------");
            logger.WriteInformation("             Process PID: " + Process.GetCurrentProcess().Id);
            logger.WriteInformation("          Entry assembly: " + Assembly.GetEntryAssembly().GetName());
            logger.WriteInformation("      Executing assembly: " + Assembly.GetExecutingAssembly().GetName());
            logger.WriteInformation("Environment command line: " + Environment.CommandLine);
            logger.WriteInformation("      Application folder: " + Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath));
            logger.WriteInformation("          Working folder: " + Directory.GetCurrentDirectory());
            logger.WriteInformation("               User name: " + WindowsIdentity.GetCurrent().Name);
            logger.WriteInformation("         Current culture: " + Thread.CurrentThread.CurrentCulture.Name);
            logger.WriteInformation("      Current UI culture: " + Thread.CurrentThread.CurrentUICulture.Name);
            logger.WriteInformation("    Log file appender(s): " + Log4NetConfigurator.LogFileAppenders());
            logger.WriteInformation("---------------------------------------------------------------------------");
        }

        private static void Tears()
        {
            ILogger logger = new Log4NetLoggerFactory(Assembly.GetExecutingAssembly()).Create("tears");

            logger.WriteInformation("---------------------------------------------------------------------------");
            logger.WriteInformation("Application is down");
            logger.WriteInformation("---------------------------------------------------------------------------");
        }
    }
}
