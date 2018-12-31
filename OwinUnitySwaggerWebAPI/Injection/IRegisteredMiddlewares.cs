using System;
using System.Collections.Generic;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public interface IRegisteredMiddlewares
    {
        IEnumerable<Type> GetMiddlewares();
    }
}
