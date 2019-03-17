using System;
using System.Collections.Generic;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public interface ITypeProvider<T> where T : class
    {
        IEnumerable<Type> GetTypes();
    }
}
