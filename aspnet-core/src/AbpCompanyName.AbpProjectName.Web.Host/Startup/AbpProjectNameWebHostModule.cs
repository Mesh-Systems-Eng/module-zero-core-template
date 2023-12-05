#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpCompanyName.AbpProjectName.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace AbpCompanyName.AbpProjectName.Web.Host.Startup;

[System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Initial framework.")]
[DependsOn(
   typeof(AbpProjectNameWebCoreModule))]
public class AbpProjectNameWebHostModule(IWebHostEnvironment env) : AbpModule
{
    private readonly IWebHostEnvironment _env = env;
    private readonly IConfigurationRoot _appConfiguration = env.GetAppConfiguration();

    public override void Initialize() =>
        IocManager.RegisterAssemblyByConvention(typeof(AbpProjectNameWebHostModule).GetAssembly());
}
