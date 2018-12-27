using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Microsoft.AspNet.SignalR.Hubs;
using Unity;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public class RegisteredHubs : IRegisteredHubs
    {
        protected readonly IRegisteredTypesSelector<IHub> _selector;

        public RegisteredHubs(IUnityContainer container)
        {
            _selector = new RegisteredTypesSelector<IHub>(container);
        }

        public IEnumerable<Type> GetHubs()
        {
            return _selector.GetTypes();
        }
    }
}
