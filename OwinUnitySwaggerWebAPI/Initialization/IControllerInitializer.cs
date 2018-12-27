using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinUnitySwaggerWebAPI.Initialization
{
    public interface IControllerInitializer
    {
        void OneTimeStartup();
        void OneTimeShutdown();
    }
}
