using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR.Hubs;
using OwinUnitySwaggerWebAPI.Injection;

namespace OwinUnitySwaggerWebAPI.Hubs
{
    internal class HubDescriptorProvider : IHubDescriptorProvider
    {
        private readonly IRegisteredHubs _registeredHubs;
        private readonly IDictionary<string, HubDescriptor> _hubsDescriptorCache;

        public HubDescriptorProvider(IRegisteredHubs registeredHubs)
        {
            _registeredHubs = registeredHubs;
            _hubsDescriptorCache = CreateCache();
        }

        public IList<HubDescriptor> GetHubs()
        {
            return _hubsDescriptorCache.Values.Distinct().ToList();
        }

        public bool TryGetHub(string hubName, out HubDescriptor descriptor)
        {
            return _hubsDescriptorCache.TryGetValue(hubName, out descriptor);
        }

        protected IDictionary<string, HubDescriptor> CreateCache()
        {
            IEnumerable<Type> hubs = _registeredHubs.GetHubs();
            IEnumerable<HubDescriptor> hubDescriptors = hubs.Select(x => new HubDescriptor
                                                                         {
                                                                             NameSpecified = GetHubAttributeName(x) != null,
                                                                             Name = GetHubName(x),
                                                                             HubType = x,
                                                                         });

            IDictionary<string, HubDescriptor> cache = new Dictionary<string, HubDescriptor>(StringComparer.Ordinal);
            foreach (HubDescriptor descriptor in hubDescriptors)
            {
                HubDescriptor cachedDescriptor;
                if (!cache.TryGetValue(descriptor.Name, out cachedDescriptor))
                {
                    cache[descriptor.Name] = descriptor;
                }
                else
                {
                    throw new InvalidOperationException("Hubs cannot have the same name='" + descriptor.Name + "' in assembly1='" + descriptor.HubType.AssemblyQualifiedName + "' and in assembly2='" + cachedDescriptor.HubType.AssemblyQualifiedName + "'");
                }
            }

            return cache;
        }

        private string GetHubName(Type type)
        {
            if (!typeof(IHub).IsAssignableFrom(type))
            {
                return null;
            }

            return GetHubAttributeName(type) ?? GetHubTypeName(type);
        }

        private string GetHubTypeName(Type type)
        {
            int index = type.Name.IndexOf('`'); // Extract only the generic name for generic CLR types.
            return index >= 0
                       ? type.Name.Substring(0, index)
                       : type.Name;
        }

        private string GetHubAttributeName(Type type)
        {
            if (typeof(IHub).IsAssignableFrom(type))
            {
                return null;
            }

            return ReflectionHelper.GetAttributeValue(type, (HubNameAttribute attr) => attr.HubName);
        }
    }
}
