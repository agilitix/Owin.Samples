using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Unity;

namespace OwinUnitySwaggerWebAPI
{
    public class UnityActionDescriptorFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {
        private readonly IUnityContainer _container;

        public UnityActionDescriptorFilterProvider(IUnityContainer container)
        {
            _container = container;
        }

        public new IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            FilterInfo[] filters = base.GetFilters(configuration, actionDescriptor).ToArray();
            foreach (FilterInfo filter in filters)
            {
                _container.BuildUp(filter.Instance.GetType(), filter.Instance);
            }

            return filters;
        }
    }
}
