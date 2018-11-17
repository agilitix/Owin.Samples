using System;
using Microsoft.Owin.Hosting;
using OwinUnitySwaggerWebAPI.Injection;
using OwinUnitySwaggerWebAPI.Logging;
using Unity;

namespace OwinUnitySwaggerWebAPI
{
    public class Server : IDisposable
    {
        protected readonly IUnityProvider _unityProvider;
        protected IDisposable _webApp;

        public Server(IUnityProvider unityProvider)
        {
            Log4NetConfigurator.Configure();

            _unityProvider = unityProvider;
            Startup.Container = _unityProvider.Container;
        }

        public void Start(string baseURL)
        {
            _webApp = WebApp.Start<Startup>(baseURL);
        }

        public void Dispose()
        {
            _webApp?.Dispose();
            _webApp = null;

            Startup.Container = null;
        }
    }
}
