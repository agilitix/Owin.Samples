using System;
using System.Collections.Generic;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public interface IRegisteredHubs
    {
        IEnumerable<Type> GetHubs();
    }
}
