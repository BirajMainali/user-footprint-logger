using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Tracker.Manager;
using Tracker.Models;

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
            if (context.Request.Path.Value != null && !context.Request.Path.Value.Equals("/"))
            {
                context.Response.OnStarting(async () =>
                {
                    if (context.Response.StatusCode != StatusCodes.Status404NotFound)
                    {
                        var footPrint = new FootPrint
                        {
                            Path = context.Request.Path.Value,
                            Method = context.Request.Method,
                            Ip = context.Connection.RemoteIpAddress?.ToString(),
                            User = context.User.Identity?.Name,
                            RecDate = DateTime.Now.ToShortDateString(),
                            Data = context.Request.QueryString.Value
                        };
                        await TraceManger.Save(footPrint);
                    }
                });
            }

            await _next.Invoke(context);
        }
    }
}