using ibreca_data_access.Contexts.CloudinaryAPI;
using ibreca_data_access.Contexts.IbrecaDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace ibreca_web_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string MainConnectionString = Configuration.GetConnectionString("MainConnection");
            services.AddDbContextPool<IbrecaDBContext>(options => options.UseSqlServer(MainConnectionString));
            services.AddSingleton(new CloudinaryAPI());

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.KnownProxies.Add(IPAddress.Parse("129.158.58.121"));
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
                options =>
                    options
                        .WithOrigins(
                            "http://localhost:3000/", "http://10.0.0.144:3000/", "http://laptop-ugasvm9m:3000/",
                            "https://*.ibreca-public-web-app-v2.pages.dev", "https://admin.ibreca.org",
                            "https://*.ibreca-public-web-app-v2.pages.dev/", "https://www.ibreca.org", "https://ibreca.org"
                        )
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .SetIsOriginAllowed(origin => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
            );

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
