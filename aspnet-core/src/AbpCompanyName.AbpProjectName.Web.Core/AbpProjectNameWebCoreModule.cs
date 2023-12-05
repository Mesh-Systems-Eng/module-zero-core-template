#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using AbpCompanyName.AbpProjectName.Authentication.JwtBearer;
using AbpCompanyName.AbpProjectName.Configuration;
using AbpCompanyName.AbpProjectName.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace AbpCompanyName.AbpProjectName;

[DependsOn(
    typeof(AbpProjectNameApplicationModule),
    typeof(AbpProjectNameEntityFrameworkModule),
    typeof(AbpAspNetCoreModule),
    typeof(AbpAspNetCoreSignalRModule))
    ]
public class AbpProjectNameWebCoreModule(IWebHostEnvironment env) : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration = env.GetAppConfiguration();

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            AbpProjectNameConsts.ConnectionStringName);

        // Use database for language management
        Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

        Configuration.Modules.AbpAspNetCore()
             .CreateControllersForAppServices(
                 typeof(AbpProjectNameApplicationModule).GetAssembly());

        ConfigureTokenAuth();
    }

    public override void Initialize() =>
        IocManager.RegisterAssemblyByConvention(typeof(AbpProjectNameWebCoreModule)
            .GetAssembly());

    public override void PostInitialize() =>
        IocManager.Resolve<ApplicationPartManager>()
        .AddApplicationPartsIfNotAddedBefore(typeof(AbpProjectNameWebCoreModule).Assembly);

    private void ConfigureTokenAuth()
    {
        IocManager.Register<TokenAuthConfiguration>();
        var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

        tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
        tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
        tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
        tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
        tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
    }
}
