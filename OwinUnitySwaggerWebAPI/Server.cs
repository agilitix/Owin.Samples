using System;
using Microsoft.Owin.Hosting;
using Unity;

namespace OwinUnitySwaggerWebAPI
{
    public class Server : IDisposable
    {
        protected IDisposable _webApp;

        public IUnityContainer Container { get; private set; }

        public Server(string unityConfigFile = "unity.config", string unityContainerName = "")
        {
            UnityConfig.LoadContainer(unityConfigFile, unityContainerName);
            Container = UnityConfig.Container;
        }

        public void Start(string baseURL)
        {
            _webApp = WebApp.Start<Startup>(baseURL);
        }

        public void Dispose()
        {
            _webApp?.Dispose();
            _webApp = null;

            Container?.Dispose();
            Container = null;
        }
    }
}
