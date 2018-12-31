using System;
using System.Collections.Generic;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public interface IRegisteredTypesSelector<T> where T : class
    {
        IEnumerable<Type> GetTypes();
    }
}
