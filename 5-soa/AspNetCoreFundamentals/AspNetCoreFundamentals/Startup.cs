using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using AspNetCoreFundamentals.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCoreFundamentals
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // use reflection to find all controllers and be able to access them by name
            // (assumes every controller has [Route("[controller]")]  )
            //Dictionary<string, Type> controllerMap = typeof(Startup).Assembly.GetTypes()
            //    .Where(t => t.IsClass && t.Name.EndsWith("Controller"))
            //    .ToDictionary(t => t.Name[..^10].ToLower());

            //services.AddSingleton(controllerMap);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // here we plug in middleware in a careful order to form the request pipeline.
            // each one gets a chance to inspect the request, possibly short-circuit and finish the response,
            //  possibly just configure a little of the response and let the rest of the middleware finish it,
            //  possibly do nothing, etc

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();

                app.Use(async (context, next) =>
                {
                    // manual exception-handling middleware example
                    try
                    {
                        await next();
                    }
                    catch (Exception e)
                    {
                        //var logger = context.RequestServices.GetRequiredService<ILogger>();
                        //// log the exception
                        //logger.LogError(e, "exception caught by manual exception-handling middleware");

                        if (!context.Response.HasStarted)
                        {
                            context.Response.StatusCode = 500;
                            context.Response.ContentType = MediaTypeNames.Application.Json;
                            await context.Response.WriteAsync("\"unexpected server error\"");
                        }
                    }
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });

            //app.Use(async (context, next) =>
            //{
            //    var controllers = context.RequestServices.GetRequiredService<Dictionary<string, Type>>();

            //    var request = context.Request;

            //    if (controllers.TryGetValue(request.Path.Value[1..], out Type controllerType))
            //    {
            //        // get zero-arg ctor, call it with zero args
            //        DataController controller = (DataController)controllerType.GetConstructor(new Type[0]).Invoke(new object[0]);

            //        // should do more reflection, but just assume it's this method
            //        object data = controller.GetData();

            //        context.Response.StatusCode = 200;
            //        context.Response.ContentType = "application/json";

            //        await JsonSerializer.SerializeAsync(context.Response.Body, data);
            //    }
            //    else
            //    {
            //        // 404
            //        await next();
            //    }
            //});

            //app.Use(async (context, next) =>
            //{
            //    throw new InvalidOperationException("oops");

            //    var request = context.Request;

            //    string data = "default";

            //    if (request.Path == "/data1")
            //    {
            //        data = "asdf";
            //    }
            //    else if (request.Path == "/data2")
            //    {
            //        data = request.Query["data"];
            //    }
            //    else
            //    {
            //        await next();
            //        return;
            //    }

            //    context.Response.StatusCode = 200;
            //    context.Response.ContentType = "application/json";
            //    await context.Response.WriteAsync($"{{ \"data\": \"{data}\" }}");
            //});

            app.Run(async context =>
            {
                var request = context.Request;

                context.Response.StatusCode = 404;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(@"<!doctype html>
page not found
");
            });

            //app.Run(context =>
            //{
            //    var request = context.Request;

            //    context.Response.StatusCode = 204;

            //    return Task.CompletedTask;
            //});
        }
    }
}
