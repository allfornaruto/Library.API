using Library.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Library.API.Extentions
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpMethodCheckMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpMethodCheckMiddleware>();
        }

    }
}
