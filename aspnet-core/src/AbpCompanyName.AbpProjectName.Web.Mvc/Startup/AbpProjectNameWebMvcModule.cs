#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpCompanyName.AbpProjectName.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace AbpCompanyName.AbpProjectName.Web.Startup;

[DependsOn(typeof(AbpProjectNameWebCoreModule))]
[SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Keeping for future use.")]
public class AbpProjectNameWebMvcModule(IWebHostEnvironment env) : AbpModule
{
    private readonly IWebHostEnvironment _env = env;
    private readonly IConfigurationRoot _appConfiguration = env.GetAppConfiguration();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void PreInitialize() =>
        Configuration.Navigation.Providers.Add<AbpProjectNameNavigationProvider>();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Initialize() =>
        IocManager.RegisterAssemblyByConvention(typeof(AbpProjectNameWebMvcModule).GetAssembly());
}
