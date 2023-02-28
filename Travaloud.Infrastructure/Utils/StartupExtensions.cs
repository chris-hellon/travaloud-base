using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Travaloud.Core.Interfaces.Services;
using Travaloud.Core.Models;
using Travaloud.Core.Data;
using Travaloud.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Rollbar;
using Rollbar.NetCore.AspNet;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using System.IO.Compression;
using Microsoft.Extensions.Hosting;
using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Travaloud.Infrastructure.Utils
{
	public static class StartupExtensions
    {
        public static IServiceCollection AddTravaloudIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddUserManager<ApplicationUserManager<ApplicationUser>>().AddRoles<ApplicationRole>().AddDefaultUI().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.MaxAge = TimeSpan.FromDays(365);
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.SlidingExpiration = true;
                options.Cookie.Name = "fuse-hostels-and-travel-auth";
            });

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>();

            return services;
        }

        public static IServiceCollection AddTravaloudServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddTravaloudDefaultConfiguration(configuration, env);
            services.AddTravaloudDefaultServices();

            if (!env.IsDevelopment())
                services.AddRolbarConfiguration(configuration);

            if (configuration.GetSection("ReCaptchaV2") != null)
                services.AddReCaptcha(configuration.GetSection("ReCaptchaV2"));

            return services;
        }

        public static IServiceCollection AddTravaloudDefaultServices(this IServiceCollection services)
        {
            services.AddScoped<IDapperContext, DapperContext>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IPropertiesRepository, PropertiesRepository>();
            services.AddScoped<IToursRepository, ToursRepository>();
            services.AddScoped<IEventsRepository, EventsRepository>();
            services.AddScoped<IBookingsRepository, BookingsRepository>();
            services.AddScoped<IErrorLoggerService, ErrorLoggerService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IRazorPartialToStringRenderer, RazorPartialToStringRenderer>();

            return services;
        }

        public static IServiceCollection AddTravaloudDefaultConfiguration(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            var travaloudConfiguration = configuration.GetSection("TravaloudConfiguration");

            if (!travaloudConfiguration.Exists())
                throw new Exception("No Travaloud Configuration provided in appsettings.json.");

            var propertyBookingUrl = "property-booking";
            if (travaloudConfiguration["PropertyBookingUrl"] != null)
                propertyBookingUrl = travaloudConfiguration["PropertyBookingUrl"].ToString();

            services.AddRazorPages().AddRazorRuntimeCompilation().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/PropertyBooking/Index", propertyBookingUrl + "/{propertyName}/{checkInDate?}/{checkOutDate?}/{userId?}");
            });

            services.AddHttpContextAccessor();

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = Enumerable.Empty<string>();
                options.MimeTypes.Append("image/png");
                options.MimeTypes.Append("image/webp");
                options.MimeTypes.Append("image/jpg");
                options.MimeTypes.Append("image/jpeg");
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCaching();

            return services;
        }

        public static IApplicationBuilder ConfigureTravaloudApp(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
                app.UseRollbarMiddleware();
            }

            app.UseStatusCodePagesWithRedirects("/error/{0}");

            app.SetAppCulture();

            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }

        public static IApplicationBuilder SetAppCulture(this IApplicationBuilder app)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-GB");
            var dateformat = new DateTimeFormatInfo
            {
                ShortDatePattern = "dd/MM/yyyy",
                LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
            };
            culture.DateTimeFormat = dateformat;

            var supportedCultures = new[] {
                culture
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            return app;
        }

        public static IServiceCollection AddRolbarConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            RollbarInfrastructureConfig config = new RollbarInfrastructureConfig(configuration["RollbarConfig:AccessToken"], configuration["RollbarConfig:Environment"]);
            config.RollbarInfrastructureOptions.CaptureUncaughtExceptions = true;
            RollbarDataSecurityOptions dataSecurityOptions = new RollbarDataSecurityOptions()
            {
                ScrubFields = new string[] { "url", "method", }
            };
            config.RollbarLoggerConfig.RollbarDataSecurityOptions.Reconfigure(dataSecurityOptions);

            RollbarInfrastructure.Instance.Init(config);

            services.AddRollbarLogger(loggerOptions =>
            {
                loggerOptions.Filter = (loggerName, loglevel) => loglevel >= LogLevel.Error;
            });

            return services;
        }
    }
}

