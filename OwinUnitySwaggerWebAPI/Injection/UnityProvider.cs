using System.Configuration;
using Microsoft.Practices.Unity.Configuration;
using Unity;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public class UnityProvider : IUnityProvider
    {
        public IUnityContainer Container { get; private set; }

        public UnityProvider(string unityConfigFile = "unity.config", string unityContainerName = "")
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap {ExeConfigFilename = unityConfigFile};
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            Container = new UnityContainer();
            UnityConfigurationSection unitySection = (UnityConfigurationSection) configuration.GetSection("unity");
            Container.LoadConfiguration(unitySection, unityContainerName);
        }

        public void Dispose()
        {
            Container?.Dispose();
            Container = null;
        }
    }
}
