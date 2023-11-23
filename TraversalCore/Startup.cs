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


            //2 loglama kullan�lacak biri output yani derlenme an�nda console yaz�lacak di�er ise text dosyas� Output ekran�
            services.AddLogging(x =>
            {
                x.ClearProviders(); //sa�lay�c�lar varsa onlaru temizliyoruz ��nk� kendi loglar�m�z� kullancaz
                x.SetMinimumLevel(LogLevel.Debug); //LogLevel alt�ndaki metotlar log seviyesini belirtiyor (debug den itibaren ba�las�n) gibi 
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


            //project level auth process (proje seviyesinde authentication kullan�yoruz)
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });


            //localization & globalization
            services.AddLocalization(opt =>
            {
                // dil resourch dosyalar�n� hangi klas�rde arayaca��n� belirtiyoruz 
                //Resources klas�r� i�inde ki Views i�indeki klas�r dil deste�i kullacan��m�z sayfa�n� controlleru ile ayn� isimde olmal�
                opt.ResourcesPath = "Resources";
            });

            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

            // kullan�c� giri� yapmad�ysa y�nlendirilecek adres
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/SignIn";
            });
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

            var supportedCultures = new[] { "en", "tr", "de" };
            //setDefaultCultures hangisini baz alaca��n� belirtiyoruz burda en belirttik
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