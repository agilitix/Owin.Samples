using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity;

namespace OwinUnitySwaggerWebAPI
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; private set; } = new UnityContainer();
        public static IReadOnlyDictionary<string, string> Properties { get; private set; } = new Dictionary<string, string>();

        public static void LoadContainer(string containerConfigFile, string containerName = "")
        {
            UnityProvider provider = new UnityProvider(containerConfigFile, containerName);
            Container = provider.Container;

            if (Container.IsRegistered<IDictionary<string, string>>("Properties"))
            {
                IDictionary<string, string> properties = Container.Resolve<IDictionary<string, string>>("Properties");
                Properties = new ReadOnlyDictionary<string, string>(properties);
            }
        }
    }
}
