using System.Text;
using Microsoft.Owin;

namespace OwinUnitySwaggerWebAPI.Middlewares
{
    internal class ResponseInfoLog
    {
        public string ContentType { get; set; }
        public int StatusCode { get; set; }

        public ResponseInfoLog(IOwinResponse response)
        {
            ContentType = response.ContentType;
            StatusCode = response.StatusCode;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}={1}, ", nameof(ContentType), ContentType);
            sb.AppendFormat("{0}={1}", nameof(StatusCode), StatusCode);
            return sb.ToString();
        }
    }
}
