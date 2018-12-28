namespace OwinUnitySwaggerWebAPI.Common.Services
{
    public interface IRegisteredServices
    {
        bool IsRegistered<T>(string serviceName = "");
        T Resolve<T>(string serviceName = "");
        bool TryResolve<T>(out T resolved, string serviceName = "");
    }
}
