using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Unity;

namespace OwinUnitySwaggerWebAPI.Injection
{
    internal class RegisteredTypesSelector<T> : IRegisteredTypesSelector<T> where T : class
    {
        private readonly IUnityContainer _container;

        public RegisteredTypesSelector(IUnityContainer container)
        {
            _container = container;
        }

        public IEnumerable<Type> GetTypes()
        {
            IList<Type> registeredTypes = _container.Registrations
                                                    .Where(x => IsTypeOf(x.MappedToType))
                                                    .Select(x => x.MappedToType)
                                                    .ToList();
            return registeredTypes;
        }

        protected bool IsTypeOf(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return type.IsClass
                   && type.IsVisible
                   && !type.IsAbstract
                   && typeof(T).IsAssignableFrom(type);
        }
    }
}
