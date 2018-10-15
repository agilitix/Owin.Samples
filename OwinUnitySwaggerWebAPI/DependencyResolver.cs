using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace OwinUnitySwaggerWebAPI
{
    /// <summary>
    /// Services resolver based on Unity underneath.
    /// </summary>
    public class DependencyResolverxx : IDependencyResolver
    {
        private readonly IUnityContainer _container;
        private readonly DependencyScopexx _sharedScope;

        /// <summary>
        /// Construct with external container.
        /// </summary>
        public DependencyResolverxx(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            _container = container;
            _sharedScope = new DependencyScopexx(container);
        }

        /// <summary>
        /// Resolve given service type.
        /// </summary>
        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        /// Resolve all services for given type.
        /// </summary>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        /// Scoped dependencies resolver.
        /// </summary>
        public IDependencyScope BeginScope()
        {
            return _sharedScope;
        }

        /// <summary>
        /// Cleanup, all registered disposables will be disposed as well.
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
            _sharedScope.Dispose();
        }
    }

    /// <summary>
    /// Scoped resolver.
    /// </summary>
    internal class DependencyScopexx : IDependencyScope
    {
        private readonly IUnityContainer _unity;

        public DependencyScopexx(IUnityContainer unity)
        {
            _unity = unity;
        }

        public object GetService(Type serviceType)
        {
            return _unity.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _unity.ResolveAll(serviceType);
        }

        public void Dispose()
        {
            // Nothing to do since we share the underneath container.
        }
    }
}
