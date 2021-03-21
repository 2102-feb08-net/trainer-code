using System.Collections.Generic;
using System.Linq;
using EmailApp.Business;
using EmailApp.Business.TypiCode;
using EmailApp.DataAccess;
using EmailApp.DataAccess.EfModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace EmailApp.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("EmailDb");

            services.AddDbContext<EmailContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IInboxCleaner, InboxCleaner>();

            services.AddHttpClient<TypiCodeService>();

            // most of these strings would be better read from configuration,
            // since we might want them to vary from one environment to another
            // like the connection string can

            services.AddCors(options =>
                options.AddDefaultPolicy(config => config
                    .WithOrigins(
                        "http://localhost:4200",
                        "https://2102-escalona-email-ui.azurewebsites.net")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://dev-723797.okta.com/oauth2/default";
                    options.Audience = "api://default";
                });

            services.AddAuthorization(options =>
            {
                // all action methods without [AllowAnonymous] or [Authorize(...)]
                // will default to [Authorize], i.e. must be authenticated
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                // this could be a separate authorization handler class
                // (more unit testable, could access dependency injection (e.g. repository) if needed)
                options.AddPolicy("AllowedAddresses", policy => policy.RequireAssertion(context =>
                {
                    var allowed = (IEnumerable<string>)context.Resource;
                    string userAddress = context.User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;

                    return allowed.Contains(userAddress);
                }));
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmailApp.WebUI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmailApp.WebUI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
