﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

// Note: The using Abp.AspNetCore.SignalR.Hubs statement needs added when using SignalR.;

using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Json;
using AbpCompanyName.AbpProjectName.Authentication.JwtBearer;
using AbpCompanyName.AbpProjectName.Configuration;
using AbpCompanyName.AbpProjectName.Configuration.Options;
using AbpCompanyName.AbpProjectName.Identity;
using AbpCompanyName.AbpProjectName.Web.Resources;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.WebEncoders;
using Newtonsoft.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace AbpCompanyName.AbpProjectName.Web.Startup;

public class Startup(IWebHostEnvironment env)
{
    private readonly IWebHostEnvironment _hostingEnvironment = env;
    private readonly IConfigurationRoot _appConfiguration = env.GetAppConfiguration();

    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureBackgroundWorkerTimer(_appConfiguration);

        // MVC
        services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
                })
            .AddRazorRuntimeCompilation()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new AbpMvcContractResolver(IocManager.Instance)
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

        IdentityRegistrar.Register(services);
        AuthConfigurer.Configure(services, _appConfiguration);

        services.Configure<WebEncoderOptions>(options =>
        {
            options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
        });

        services.AddScoped<IWebResourceManager, WebResourceManager>();

        // Uncomment to add SignalR hubs
        // services.AddSignalR();

        // Configure Abp and Dependency Injection
        // Configure Log4Net logging
        services.AddAbpWithoutCreatingServiceProvider<AbpProjectNameWebMvcModule>(
            options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(_hostingEnvironment.IsDevelopment()
                ? "log4net.config"
                : "log4net.Production.config")));

        services
            .RegisterSharedServicesConfiguration(_appConfiguration)
            .RegisterEmailConfiguration(_appConfiguration)
            .RegisterSmsConfiguration(_appConfiguration);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Initial framework.")]
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        app.UseAbp(); // Initializes ABP framework.

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();

        app.UseJwtTokenMiddleware();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            // Uncomment to add SignalR hubs
            // endpoints.MapHub<AbpCommonHub>("/signalr");
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
        });
    }
}
