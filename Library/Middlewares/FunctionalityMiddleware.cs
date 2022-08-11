using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Library.Services;
using Library.Interface;

namespace Library.Middlewares
{
    public class FunctionalityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IFunctionalityValidator _functionalityService;

        public  FunctionalityMiddleware(IFunctionalityValidator functionalityService)
        {
            _functionalityService = functionalityService;
        }

        public FunctionalityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            //llamar al servicio. llamar al arbol mock
            Task task = _functionalityService.ProcessInfo();
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
