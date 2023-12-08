#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using AbpCompanyName.AbpProjectName.EntityFrameworkCore.Seed;

namespace AbpCompanyName.AbpProjectName.EntityFrameworkCore;

[DependsOn(
    typeof(AbpProjectNameCoreModule),
    typeof(AbpZeroCoreEntityFrameworkCoreModule))]
public class AbpProjectNameEntityFrameworkModule : AbpModule
{
    /// <summary>
    /// Gets or sets a value indicating whether dbcontext registration should be skipped in order to use in-memory database of EF Core during testing.
    /// </summary>
    public bool SkipDbContextRegistration { get; set; }

    public bool SkipDbSeed { get; set; }

    public override void PreInitialize()
    {
        if (!SkipDbContextRegistration)
        {
            Configuration.Modules.AbpEfCore().AddDbContext<AbpProjectNameDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    AbpProjectNameDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    AbpProjectNameDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }
    }

    public override void Initialize() =>
        IocManager.RegisterAssemblyByConvention(typeof(AbpProjectNameEntityFrameworkModule).GetAssembly());

    public override void PostInitialize()
    {
        if (!SkipDbSeed)
        {
            SeedHelper.SeedHostDb(IocManager);
        }
    }
}
