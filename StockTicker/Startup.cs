using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StockTicker.Service.Data.Models;
using StockTicker.Service.Data.Services;
using CarParts.Service.Data.Models;
using Microsoft.OpenApi.Models;
using MediatR;
using System.Reflection;
using StockTicker.Domain.Commands.Data;
using StockTicker.Domain.Queries.Data;
using CarParts.Domain.Commands.Data;
using StockTicker.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace StockTicker
{
    public class Startup
    {
        public class RequireHttpsAttribute : AuthorizationFilterAttribute
        {
            public override void OnAuthorization(HttpActionContext actionContext)
            {
                if (actionContext.Request.RequestUri.Scheme != System.Uri.UriSchemeHttps)
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                    {
                        ReasonPhrase = "HTTPS Required for this call"
                    };
                }
                else
                {
                    base.OnAuthorization(actionContext);
                }
            }
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc().AddXmlSerializerFormatters();

            services.AddMediatR(typeof(GetDataAllQueryHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateCompanyCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateCarPartsCommandHandler).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(UpdateCompanyCommandHandler).GetTypeInfo().Assembly);

            services.AddSingleton(Configuration);

            services.AddScoped<ICompanyDBService, CompanyDBService>();
            string conString = Configuration.GetConnectionString("StockTickerConnection");
            services.AddDbContext<CompanyContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("StockTickerConnection")));

            /*services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(RequireHttpsAttribute));
            });*/

            services.AddIdentity<ApplicationUser, IdentityRole>();
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "CompanyContext",
                    Version = "v2",
                    Description = "Glass Lewis Code Challenge"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Glass Lewis Code Challenge");
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action}/{id?}",
                    defaults: new { controller = "AnalyticalData", action = "GetAnalyticalDataAll" });
            });
        }
    }
}
