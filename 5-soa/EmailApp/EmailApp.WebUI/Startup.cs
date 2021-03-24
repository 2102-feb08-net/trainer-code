using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
using Okta.AspNetCore;

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
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddScoped<IInboxCleaner, InboxCleaner>();

            services.AddHttpContextAccessor();

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
                    //options.SaveToken = true;
                });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
            //    options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
            //    options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
            //})
            //.AddOktaWebApi(new OktaWebApiOptions
            //{
            //    OktaDomain = "https://dev-723797.okta.com"
            //});

            services.AddAuthorization(options =>
            {
                // all action methods without [AllowAnonymous] or [Authorize(...)]
                // will default to [Authorize], i.e. must be authenticated
                if (Configuration["AuthRequired"] == "true")
                {
                    options.FallbackPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                }

                // this could be a separate authorization handler class
                // (more unit testable, could access dependency injection (e.g. repository) if needed)
                options.AddPolicy("AllowedAddresses", policy => policy.RequireAssertion(context =>
                {
                    var allowed = (IEnumerable<string>)context.Resource;
                    string userAddress = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

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

            // if this is a new user, add him
            app.Use(async (context, next) =>
            {
                // this could be a separate middleware class
                // (more unit testable, could use constructor injection)
                if (context.User.Identity.IsAuthenticated)
                {
                    
                    var userAddress = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var uow = context.RequestServices.GetRequiredService<IUnitOfWork>();
                    if (await uow.AccountRepository.AddIfNotExistsAsync(userAddress))
                    {
                        await uow.SaveAsync();
                    }
                }
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
