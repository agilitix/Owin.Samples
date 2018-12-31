using System;
using System.Collections.Generic;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public interface IRegisteredControllers
    {
        IEnumerable<Type> GetControllers();
    }
}
