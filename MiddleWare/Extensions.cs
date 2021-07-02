using Microsoft.AspNetCore.Builder;

namespace Tracker.MiddleWare
{
    public static class Extensions
    {
        public static IApplicationBuilder UseCustomMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TraceManager>();
        }
    }
}