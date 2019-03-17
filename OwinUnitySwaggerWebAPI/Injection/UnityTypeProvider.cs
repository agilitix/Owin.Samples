using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace OwinUnitySwaggerWebAPI.Injection
{
    internal class UnityTypeProvider<T> : ITypeProvider<T> where T : class
    {
        private readonly IUnityContainer _container;

        public UnityTypeProvider(IUnityContainer container)
        {
            _container = container;
        }

        public IEnumerable<Type> GetTypes()
        {
            IList<Type> registeredTypes = _container.Registrations
                                                    .Where(x => IsExpectedType(x.MappedToType))
                                                    .Select(x => x.MappedToType)
                                                    .ToList();
            return registeredTypes;
        }

        protected bool IsExpectedType(Type type)
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
