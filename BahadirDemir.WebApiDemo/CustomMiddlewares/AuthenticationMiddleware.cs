using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BahadirDemir.WebApiDemo.CustomMiddlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;


        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];

            if (authHeader == null)
            {
                await _next(context);
                return;
            }

            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring(6).Trim();

                var credentials = token.Split(':');
                if (credentials[0] == "bahadir" && credentials[1] == "123456")
                {
                    var claims = new[]
                    {
                        new Claim("name",credentials[0]),
                        new Claim(ClaimTypes.Role,"Admin")
                    };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    context.User = new ClaimsPrincipal(identity);
                }
                else
                {
                    context.Response.StatusCode = 401;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
            }
            await _next(context);
        }
    }
}
