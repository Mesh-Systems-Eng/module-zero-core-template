#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpCompanyName.AbpProjectName.Authorization;

namespace AbpCompanyName.AbpProjectName;

[DependsOn(
    typeof(AbpProjectNameCoreModule),
    typeof(AbpAutoMapperModule))]
public class AbpProjectNameApplicationModule : AbpModule
{
    public override void PreInitialize() =>
        Configuration.Authorization.Providers.Add<AbpProjectNameAuthorizationProvider>();

    public override void Initialize()
    {
        var thisAssembly = typeof(AbpProjectNameApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        // Scan the assembly for classes which inherit from AutoMapper.Profile
        Configuration
            .Modules
            .AbpAutoMapper()
            .Configurators
            .Add(cfg => cfg.AddMaps(thisAssembly));
    }
}
