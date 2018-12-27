using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinUnitySwaggerWebAPI
{
    public interface IServer : IDisposable
    {
        void Start(string baseUrl);
    }
}
