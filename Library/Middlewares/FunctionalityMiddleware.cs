using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Library.Services;

namespace Library.Middlewares
{
    public class FunctionalityMiddleware
    {
        private readonly RequestDelegate _next;

        public FunctionalityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task<Task> Invoke(HttpContext httpContext)
        {
            //llamar al servicio. // calltomock
            FunctionalityService functionalityService;
            await functionalityService.ProcessInfo();
            return _next(httpContext);
        }
    }

    public static class FuncionalityMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FunctionalityMiddleware>();
        }
    }
}
