using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace OwinUnitySwaggerWebAPI
{
    /// <summary>
    /// Assemblies resolved based on assemblies file name.
    /// </summary>
    public class FileAssembliesResolver : DefaultAssembliesResolver
    {
        private readonly string[] _assemblyFiles;

        /// <summary>
        /// Construct with file names of the assemblies being resolved.
        /// </summary>
        public FileAssembliesResolver(string[] assemblyFiles)
        {
            _assemblyFiles = assemblyFiles;
        }

        /// <summary>
        /// Return the resolved assemblies, the AppDomain.CurrentDomain assemblies are returned as well.
        /// </summary>
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> baseAssemblies = base.GetAssemblies();
            IList<Assembly> assemblies = new List<Assembly>(baseAssemblies);
            foreach (string assemblyFile in _assemblyFiles)
            {
                Assembly loadedAssembly = Assembly.LoadFrom(assemblyFile);
                assemblies.Add(loadedAssembly);
            }
            return assemblies;
        }
    }
}
