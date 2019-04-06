using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backEnd.Infra;
using backEnd.Models;
using ExtCore.WebApplication.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace backEnd
{
    public class Startup
    {
        private string extensionsPath;

        public Startup(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.extensionsPath = hostingEnvironment.ContentRootPath + configuration["Extensions:Path"];
            Configuration = configuration;
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddExtCore(extensionsPath);
           
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            var connection = "Data Source=goldpi.com;Initial Catalog=CTZIT_SAASMaster;user id=saasmauser;password=%746FSr&*();";
           // var connection2 = "server=3.17.110.191;database=default;uid=root1;pwd=1qaz2wsx";
            var connection2 = "server=3.17.110.191;database=default;user id=root1;password=1qaz2wsx";
            services.AddDbContext<BackEndContext>
                (options => options.UseSqlServer(connection));

            services.AddMultitenancy<AppTenant, AppTenantResolver>();
            services.AddDbContext<TeanantContext>
                (options => options.UseMySQL(connection2));
           // services.AddTransient<IDesignTimeDbContextFactory<TeanantContext>, DbContextFactory>();
            // services.AddScoped(provider =>
            // {
            //     var factory = provider.GetService<IDesignTimeDbContextFactory<TeanantContext>>();
            //     return factory.CreateDbContext(null);
            // });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddDbContext<TeanantContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("TeanantContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
           
                
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            
            app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            //app.UsePathBase
            app.UseExtCore();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMultitenancy<AppTenant>();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
