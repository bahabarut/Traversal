using BusinessLayer.Container;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using TraversalCore.Models;

namespace TraversalCore
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
            //2 loglama kullan�lacak biri output yani derlenme an�nda console yaz�lacak di�er ise text dosyas� Output ekran�
            services.AddLogging(x =>
            {
                x.ClearProviders(); //sa�lay�c�lar varsa onlaru temizliyoruz ��nk� kendi loglar�m�z� kullancaz
                x.SetMinimumLevel(LogLevel.Debug); //LogLevel alt�ndaki metotlar log seviyesini belirtiyor (debug den itibaren ba�las�n) gibi 
                x.AddDebug();
            });


            //this code for Identity
            services.AddDbContext<Context>();
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<Context>();
            // end Identity codes

            //For API requests response process
            services.AddHttpClient();

            services.ContainerDependencies();
            services.CustomValidator();

            services.AddAutoMapper(typeof(Startup));


            services.AddControllersWithViews().AddFluentValidation();


            //project level auth process (proje seviyesinde authentication kullan�yoruz)
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //serilog.extension k�t�phanesini kuruyoruz Logs klas�r�n� kendi olu�turuyor ana dizinde
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log1.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
