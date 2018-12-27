using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public interface IRegisteredTypesSelector<T> where T : class
    {
        IEnumerable<Type> GetTypes();
    }
}
