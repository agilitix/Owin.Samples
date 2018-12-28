using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace OwinUnitySwaggerWebAPI.Middlewares
{
    internal class RequestInfoLog
    {
        public string Uri { get; set; }
        public string Method { get; set; }
        public string ContentType { get; set; }
        public string Accept { get; set; }
        public string User { get; set; }
        public string RemoteIpAddress { get; set; }
        public int? RemotePort { get; set; }

        public RequestInfoLog(IOwinRequest request)
        {
            Uri = request.Uri.ToString();
            Method = request.Method;
            ContentType = request.ContentType;
            Accept = request.Accept;
            User = request.User?.Identity?.Name ?? "#N/A";
            RemoteIpAddress = request.RemoteIpAddress;
            RemotePort = request.RemotePort ?? -1;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}={1}, ", nameof(Uri), Uri);
            sb.AppendFormat("{0}={1}, ", nameof(Method), Method);
            sb.AppendFormat("{0}={1}, ", nameof(ContentType), ContentType);
            sb.AppendFormat("{0}={1}, ", nameof(Accept), Accept);
            sb.AppendFormat("{0}={1}, ", nameof(User), User);
            sb.AppendFormat("{0}={1}, ", nameof(RemoteIpAddress), RemoteIpAddress);
            sb.AppendFormat("{0}={1}", nameof(RemotePort), RemotePort);
            return sb.ToString();
        }
    }
}
