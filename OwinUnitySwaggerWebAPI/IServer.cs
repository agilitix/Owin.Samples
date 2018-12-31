using System;

namespace OwinUnitySwaggerWebAPI
{
    public interface IServer : IDisposable
    {
        void Start(string baseUrl);
    }
}
