using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Unity;

namespace OwinUnitySwaggerWebAPI
{
    public class Server
    {
        protected IDisposable _webApp;
        protected UnityProvider _unityProvider = new UnityProvider();

        public IUnityContainer Container => _unityProvider.Unity;

        public void LoadContainer(string unityConfigFile)
        {
            _unityProvider.LoadUnityConfiguration(unityConfigFile);
        }

        public void Start(string baseURL)
        {
            _webApp = WebApp.Start<Startup>(baseURL);
        }

        public void Stop()
        {
            _webApp?.Dispose();
            Container.Dispose();
        }
    }
}
