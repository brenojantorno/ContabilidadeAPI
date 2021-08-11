using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using WebApp.Security;

namespace WebApp
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISessionUser _sessao;


        public AuthenticationMiddleware(RequestDelegate next, ISessionUser sessao)
        {
            _next = next;
            _sessao = sessao;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //var authorization = context.Request.Cookies["WebAppAuthorization"];

            //if (!string.IsNullOrEmpty(authorization))
            //{
            //    context.Request.Headers["Authorization"] = "Bearer " + authorization;
            //}

            await _next.Invoke(context);
        }
    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}