using Unity;

namespace OwinUnitySwaggerWebAPI.Common.Services
{
    public class RegisteredServices : IRegisteredServices
    {
        private readonly IUnityContainer _container;

        public RegisteredServices(IUnityContainer container)
        {
            _container = container;
        }

        public bool IsRegistered<T>(string serviceName = "")
        {
            return string.IsNullOrWhiteSpace(serviceName)
                       ? _container.IsRegistered<T>()
                       : _container.IsRegistered<T>(serviceName);
        }

        public T Resolve<T>(string serviceName = "")
        {
            return string.IsNullOrWhiteSpace(serviceName)
                       ? _container.Resolve<T>()
                       : _container.Resolve<T>(serviceName);
        }

        public bool TryResolve<T>(out T resolved, string serviceName = "")
        {
            if (IsRegistered<T>(serviceName))
            {
                resolved = Resolve<T>(serviceName);
                return true;
            }

            resolved = default(T);
            return false;
        }
    }
}
