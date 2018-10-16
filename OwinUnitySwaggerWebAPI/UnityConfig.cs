using System;
using System.Collections.Generic;
using Unity;

namespace OwinUnitySwaggerWebAPI
{
    public static class UnityConfig
    {
        private static readonly IDictionary<string, IUnityContainer> _containers = new Dictionary<string, IUnityContainer>();

        public static void LoadContainer(string containerConfigFile, string containerName = "")
        {
            UnityProvider provider = new UnityProvider(containerConfigFile, containerName);
            _containers.Add(containerName, provider.Unity);
        }

        public static IUnityContainer GetContainer(string containerName = "")
        {
            IUnityContainer container;
            if (_containers.TryGetValue(containerName, out container))
            {
                return container;
            }

            throw new ArgumentException(nameof(containerName));
        }

        public static void Dispose()
        {
            foreach (IUnityContainer container in _containers.Values)
            {
                container.Dispose();
            }

            _containers.Clear();
        }
    }
}
