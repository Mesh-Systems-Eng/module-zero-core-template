#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Extensions;
using Abp.Json;
using AbpCompanyName.AbpProjectName.Configuration;
using AbpCompanyName.AbpProjectName.Configuration.Options;
using AbpCompanyName.AbpProjectName.Identity;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AbpCompanyName.AbpProjectName.Web.Host.Startup;

public class Startup(IWebHostEnvironment env)
{
    private const string DefaultCorsPolicyName = "localhost";
    private const string ApiVersion = "v1";

    private readonly IConfigurationRoot _appConfiguration = env.GetAppConfiguration();
    private readonly IWebHostEnvironment _hostingEnvironment = env;

    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureBackgroundWorkerTimer(_appConfiguration);

        // MVC
        services.AddControllersWithViews(
            options => { options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute()); })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new AbpMvcContractResolver(IocManager.Instance)
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

        IdentityRegistrar.Register(services);
        AuthConfigurer.Configure(services, _appConfiguration);

        // Uncomment to add SignalR hubs
        // services.AddSignalR();

        // Configure CORS for angular2 UI
        // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
        services.AddCors(
            options => options.AddPolicy(
                DefaultCorsPolicyName,
                builder => builder
                    .WithOrigins(
                        _appConfiguration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));

        // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
        ConfigureSwagger(services);

        // Configure Abp and Dependency Injection
        // Configure Log4Net logging
        services.AddAbpWithoutCreatingServiceProvider<AbpProjectNameWebHostModule>(
            options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(_hostingEnvironment.IsDevelopment()
                ? "log4net.config"
                : "log4net.Production.config")));
    }

    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Keeping for expected method signature.")]
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

        app.UseCors(DefaultCorsPolicyName); // Enable CORS!

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseAbpRequestLocalization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<AbpCommonHub>("/signalr");
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
        });

        // Enable middleware to serve generated Swagger as a JSON endpoint
        app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

        // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
        app.UseSwaggerUI(options =>
        {
            // specifying the Swagger JSON endpoint.
            options.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", $"AbpProjectName API {ApiVersion}");
            options.IndexStream = () => Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("AbpCompanyName.AbpProjectName.Web.Host.wwwroot.swagger.ui.index.html");
            options.DisplayRequestDuration(); // Controls the display of the request duration (in milliseconds) for "Try it out" requests.
        }); // URL: /swagger
    }

    private void ConfigureSwagger(IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(ApiVersion, new OpenApiInfo
            {
                Version = ApiVersion,
                Title = "AbpProjectName API",
                Description = "AbpProjectName",
                // TermsOfService = new Uri("https://example.com/terms"), // uncomment if needed
                Contact = new OpenApiContact
                {
                    Name = "AbpProjectName",
                    Email = string.Empty,
                    Url = new Uri("https://twitter.com/aspboilerplate"),
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/LICENSE"),
                }
            });
            options.DocInclusionPredicate((docName, description) => true);

            // Define the BearerAuth scheme that's in use
            options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            // add summaries to swagger
            var canShowSummaries = _appConfiguration.GetValue<bool>("Swagger:ShowSummaries");
            if (canShowSummaries)
            {
                var hostXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var hostXmlPath = Path.Combine(AppContext.BaseDirectory, hostXmlFile);
                options.IncludeXmlComments(hostXmlPath);

                var applicationXml = $"AbpCompanyName.AbpProjectName.Application.xml";
                var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXml);
                options.IncludeXmlComments(applicationXmlPath);

                var webCoreXmlFile = $"AbpCompanyName.AbpProjectName.Web.Core.xml";
                var webCoreXmlPath = Path.Combine(AppContext.BaseDirectory, webCoreXmlFile);
                options.IncludeXmlComments(webCoreXmlPath);
            }
        });
}
