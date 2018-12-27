using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Unity;

namespace OwinUnitySwaggerWebAPI.Injection
{
    public class RegisteredControllers : IRegisteredControllers
    {
        protected readonly IRegisteredTypesSelector<IHttpController> _selector;

        public RegisteredControllers(IUnityContainer container)
        {
            _selector = new RegisteredTypesSelector<IHttpController>(container);
        }

        public IEnumerable<Type> GetControllers()
        {
            return _selector.GetTypes();
        }
    }
}
