using System.Configuration;
using Microsoft.Practices.Unity.Configuration;
using Unity;

namespace OwinUnitySwaggerWebAPI
{
    /// <summary>
    /// Wrapper around Unity container.
    /// </summary>
    public class UnityProvider
    {
        /// <summary>
        /// The container.
        /// </summary>
        public IUnityContainer Unity { get; }

        /// <summary>
        /// Create with config file name.
        /// </summary>
        public UnityProvider(string unityConfigFile)
            : this(unityConfigFile, string.Empty)
        {
        }

        /// <summary>
        /// Create with config file name and container name.
        /// </summary>
        public UnityProvider(string unityConfigFile, string unityContainerName)
        {
            Unity = new UnityContainer();
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap {ExeConfigFilename = unityConfigFile};
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            UnityConfigurationSection unitySection = (UnityConfigurationSection) configuration.GetSection("unity");
            Unity.LoadConfiguration(unitySection, unityContainerName);
        }
    }
}
