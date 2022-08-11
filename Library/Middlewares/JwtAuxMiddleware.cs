using Library.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Library.Services;

namespace Library.Middlewares
{
    internal class JwtAuxMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IJwtAuxValidator _jwtAuxValidator;

        public JwtAuxMiddleware(RequestDelegate next, IConfiguration configuration, IJwtAuxValidator jwtAuxValidator)
        {
            _next = next;
            _configuration = configuration; 
            _jwtAuxValidator = jwtAuxValidator;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                 _jwtAuxValidator.ValidarToken(_configuration, token);
             
            return _next(httpContext);
        }
    }

    public static class JwtAuxMiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }

}
