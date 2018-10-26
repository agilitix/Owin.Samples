using System;
using System.Collections.Generic;
using Unity;

namespace OwinUnitySwaggerWebAPI
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; private set; }

        public static void LoadContainer(string containerConfigFile, string containerName = "")
        {
            UnityProvider provider = new UnityProvider(containerConfigFile, containerName);
            Container = provider.Container;
        }
    }
}
