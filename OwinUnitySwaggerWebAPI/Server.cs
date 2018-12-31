using System;
using System.Threading;
using Microsoft.Owin.Hosting;
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

            IRegisteredControllers registeredControllers = new RegisteredControllers(unityProvider.Container);
            _initializer = new ControllerInitializer(unityProvider.Container, registeredControllers);
        }

        public void Start(string baseUrl)
        {
            Log4NetConfigurator.Configure();

            _initializer.OneTimeStartup();

            // If you got "access denied" exception run in elevated mode or allow the port for the app.
            _webApp = WebApp.Start<Startup>(baseUrl);
        }

        public void Dispose()
        {
            IControllerInitializer init = Interlocked.Exchange(ref _initializer, null);
            init?.OneTimeShutdown();

            IDisposable webApp = Interlocked.Exchange(ref _webApp, null);
            webApp?.Dispose();
        }
    }
}
