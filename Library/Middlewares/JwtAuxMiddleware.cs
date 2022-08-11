using Library.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Middlewares
{
    internal class JwtAuxMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        public readonly IJwtAuxValidator _jwtAuxValidator;

        public JwtAuxMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration; 
            
        }

        public Task Invoke(HttpContext httpContext, IJwtAuxValidator jwtAuxValidator)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                jwtAuxValidator.ValidarToken(_configuration, token);
             
            return _next(httpContext);
        }
    }

    public static class JwtAuxMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidarJwt(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtAuxMiddleware>();
        }
    }

}
