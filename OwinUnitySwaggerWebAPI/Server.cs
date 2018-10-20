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

        public void Setup(string unityConfigFile)
        {
            UnityConfig.LoadContainer(unityConfigFile);
        }

        public void Start(string baseURL)
        {
            _webApp = WebApp.Start<Startup>(baseURL);
        }

        public void Stop()
        {
            _webApp?.Dispose();
            _webApp = null;

            UnityConfig.Dispose();
        }
    }
}
