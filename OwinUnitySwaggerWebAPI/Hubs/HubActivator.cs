using System;
using Microsoft.AspNet.SignalR.Hubs;
using Unity;

namespace OwinUnitySwaggerWebAPI.Hubs
{
    internal class HubActivator : IHubActivator
    {
        private readonly IUnityContainer _container;

        public HubActivator(IUnityContainer container)
        {
            _container = container;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            if (descriptor == null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            if (descriptor.HubType == null)
            {
                return null;
            }

            IUnityContainer childContainer = _container.CreateChildContainer();
            childContainer.RegisterInstance(typeof(HubDescriptor), descriptor);
            IHub hub = (childContainer.Resolve(descriptor.HubType)
                        ?? Activator.CreateInstance(descriptor.HubType)) as IHub;
            return hub;
        }
    }
}
