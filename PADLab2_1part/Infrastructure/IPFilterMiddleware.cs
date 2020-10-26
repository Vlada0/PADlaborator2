using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using PADLab2_1part.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PADLab2_1part.Infrastructure
{
    public class IPFilterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationOptions _applicationOptions;

        public IPFilterMiddleware(RequestDelegate next, IOptions<ApplicationOptions> options)
        {
            _next = next;
            _applicationOptions = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            string origin = context.Request.Headers["Origin"];
            int? port = null;
            if (!string.IsNullOrWhiteSpace(origin))
            {
                var uri = new Uri(origin);
                port = uri.Port;
            }

            if(port == null|| port != 49479)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }
           /* var ipAddress = context.Connection.RemoteIpAddress;
            List<string> whiteList = _applicationOptions.Whitelist;
            
            var isValidAddress = whiteList.Where(a => IPAddress.Parse(a).Equals(ipAddress)).Any();

            if (!isValidAddress)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }*/
            await _next.Invoke(context);
        }

    }
}
