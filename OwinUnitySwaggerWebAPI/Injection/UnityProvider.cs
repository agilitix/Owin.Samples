using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Practices.Unity.Configuration;
using Unity;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public class UnityProvider : IUnityProvider
    {
        private readonly bool _isContainerOwner;
        private IUnityContainer _container;

        public IUnityContainer Container
        {
            get { return _container; }
            private set { _container = value; }
        }

        public UnityProvider(IUnityContainer container)
        {
            _isContainerOwner = false;
            Container = container;
        }

        public UnityProvider(string unityConfigFile = "unity.config", string unityContainerName = "")
        {
            if (!File.Exists(unityConfigFile))
            {
                throw new FileNotFoundException("The unity config file='" + unityConfigFile + "' does not exists", unityConfigFile);
            }

            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap {ExeConfigFilename = unityConfigFile};
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            IUnityContainer container = new UnityContainer();
            UnityConfigurationSection unitySection = (UnityConfigurationSection) configuration.GetSection("unity");
            container.LoadConfiguration(unitySection, unityContainerName);

            Type[] unityProviders = container.Registrations.Where(x => typeof(IUnityProvider).IsAssignableFrom(x.MappedToType))
                                             .Select(x => x.MappedToType)
                                             .ToArray();
            if (unityProviders.Length > 0)
            {
                throw new InvalidOperationException("The type(s) {" + string.Join(", ", unityProviders.Select(x => x.FullName).Distinct()) + "} cannot be registered in unity");
            }

            _isContainerOwner = true;
            Container = container;
        }

        public void Dispose()
        {
            IUnityContainer container = Interlocked.Exchange(ref _container, null);
            if (_isContainerOwner)
            {
                container?.Dispose();
            }
        }
    }
}
