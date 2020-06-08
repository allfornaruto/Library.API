using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Middlewares
{
    public class HttpMethodCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment Environment;
        public HttpMethodCheckMiddleware(RequestDelegate requestDelegate, IHostEnvironment environment)
        {
            this._next = requestDelegate;
            Environment = environment;
        }

        public Task Invoke(HttpContext context)
        {
            Console.WriteLine("HttpMethodCheckMiddleware 当前环境:" + Environment.EnvironmentName);
            var requestMethod = context.Request.Method.ToUpper();
            if (requestMethod == HttpMethods.Get || requestMethod == HttpMethods.Head)
            {
                return _next(context);
            }
            else {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.Headers.Add("X-AllowHTTPVerb", new[] { "GET", "HEAD" });
                context.Response.WriteAsync("只支持 GET / HEAD 方法");
                return Task.CompletedTask;
            }
        }
    }
}
