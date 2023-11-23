using BusinessLayer.Container;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using TraversalCore.CQRS.Handlers.DestinationHandlers;
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
            services.AddScoped<GetAllDestinationQueryHandler>();
            services.AddScoped<GetDestinationByIdQueryHandler>();
            services.AddScoped<CreateDestinationCommanHandler>();
            services.AddScoped<DeleteDestinationCommandHandler>();
            services.AddScoped<UpdateDestinationCommandHandler>();

            services.AddMediatR(typeof(Startup));


            //2 loglama kullanýlacak biri output yani derlenme anýnda console yazýlacak diðer ise text dosyasý Output ekraný
            services.AddLogging(x =>
            {
                x.ClearProviders(); //saðlayýcýlar varsa onlaru temizliyoruz çünkü kendi loglarýmýzý kullancaz
                x.SetMinimumLevel(LogLevel.Debug); //LogLevel altýndaki metotlar log seviyesini belirtiyor (debug den itibaren baþlasýn) gibi 
                x.AddDebug();
            });




            //this code for Identity
            services.AddDbContext<Context>();
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<Context>().AddErrorDescriber<CustomIdentityValidator>().AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider).AddEntityFrameworkStores<Context>();
            // end Identity codes

            //For API requests response process
            services.AddHttpClient();

            services.ContainerDependencies();
            services.CustomValidator();

            services.AddAutoMapper(typeof(Startup));


            services.AddControllersWithViews().AddFluentValidation();


            //project level auth process (proje seviyesinde authentication kullanýyoruz)
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });


            //localization & globalization
            services.AddLocalization(opt =>
            {
                // dil resourch dosyalarýný hangi klasörde arayacaðýný belirtiyoruz 
                //Resources klasörü içinde ki Views içindeki klasör dil desteði kullacanðýmýz sayfaýný controlleru ile ayný isimde olmalý
                opt.ResourcesPath = "Resources";
            });

            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            // kullanýcý giriþ yapmadýysa yönlendirilecek adres
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/SignIn";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //serilog.extension kütüphanesini kuruyoruz Logs klasörünü kendi oluþturuyor ana dizinde
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

            var supportedCultures = new[] { "en", "tr", "de" };
            //setDefaultCultures hangisini baz alacaðýný belirtiyoruz burda en belirttik
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[1]).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);

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



// Language Localization 
//builder.Services.AddLocalization(opt =>
//{
//    opt.ResourcesPath = "Resources";
//});

//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    options.SetDefaultCulture("fr");
//    options.AddSupportedUICultures("tr", "en", "fr");
//    options.FallBackToParentUICultures = true;
//    options.RequestCultureProviders.Clear();
//});