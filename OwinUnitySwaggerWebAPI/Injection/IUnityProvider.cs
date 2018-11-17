using System;
using Unity;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public interface IUnityProvider : IDisposable
    {
        IUnityContainer Container { get; }
    }
}
