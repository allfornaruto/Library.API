
using Library.API.Entities;
using Library.API.Services;
using Library.API.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IO;
using Newtonsoft.Json;

public class TestJsonObj {
    public string a { get; set; }
    public string b { get; set; }
}

namespace Library.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<CheckAuthorExistFilterAttribute>();

            services.AddMvc(config =>
            {
                config.ReturnHttpNotAcceptable = true;
                config.Filters.Add<JsonExceptionFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddXmlSerializerFormatters();
            services.AddDbContext<LibraryDbContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                Console.WriteLine("中间件 A：开始");
                await next();
                Console.WriteLine("中间件 A：结束");
            });

            // app.UseMiddleware<HttpMethodCheckMiddleware>();
            //app.UseHttpMethodCheckMiddleware();

            var trackPackageRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync($"Hello! Route values: {string.Join(",", routeValues)}");
            });
            var routeBuilder = new RouteBuilder(app, trackPackageRouteHandler);
            routeBuilder.MapRoute("Track Package Route", "package/{operation}/{id:int}");
            routeBuilder.MapGet("hello/{name}", context =>
            {
                var name = context.GetRouteValue("name");
                return context.Response.WriteAsync($"Hi, ${name}");
            });
            var routes = routeBuilder.Build();
            app.UseRouter(routes);

            // /post/test?c=3&d=4
            var routeHandler2 = new RouteHandler(context =>
            {
                context.Request.Query.TryGetValue("c", out var c);
                context.Request.Query.TryGetValue("d", out var d);
                var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
                var bodyJson = reader.ReadToEndAsync().Result;
                var body = JsonConvert.DeserializeObject<TestJsonObj>(bodyJson);
                return context.Response.WriteAsync($"Hello! Route2 values: body = {body} body.a = {body.a} body.b = {body.b} c = {c} d = {d}");
            });
            var route2Builder = new RouteBuilder(app, routeHandler2);
            route2Builder.MapRoute("Route Post", "post/test");
            var route2 = route2Builder.Build();
            app.UseRouter(route2);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
