using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tracker.Manager;

namespace Tracker.MiddleWare
{
    public class TraceManager
    {
        private readonly RequestDelegate _next;

        public TraceManager(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var footPrint = new
            {
                Path = context.Request.Path.Value,
                Method = context.Request.Method,
                Ip = context.Connection.RemoteIpAddress?.ToString(),
                User = context.User.Identity?.Name,
                data = context.Request.QueryString.Value,
            };
            await FileManger.Save(footPrint);
            await _next.Invoke(context);
        }
    }
}