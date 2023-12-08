#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.Net.Mail;
using Abp.TestBase;
using Abp.Zero.Configuration;
using Abp.Zero.EntityFrameworkCore;
using AbpCompanyName.AbpProjectName.EntityFrameworkCore;
using AbpCompanyName.AbpProjectName.Tests.DependencyInjection;
using Castle.MicroKernel.Registration;
using NSubstitute;
using System;

namespace AbpCompanyName.AbpProjectName.Tests;

[DependsOn(
    typeof(AbpProjectNameApplicationModule),
    typeof(AbpProjectNameEntityFrameworkModule),
    typeof(AbpTestBaseModule))]
public class AbpProjectNameTestModule : AbpModule
{
    public AbpProjectNameTestModule(AbpProjectNameEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <remarks>Configuration.Modules.AbpAutoMapper().UseStaticMapper is disabled since it breaks unit tests (see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2052).</remarks>
    public override void PreInitialize()
    {
        Configuration.UnitOfWork.Timeout = TimeSpan.FromMinutes(30);
        Configuration.UnitOfWork.IsTransactional = false;

#pragma warning disable CS0618 // Type or member is obsolete
        // Disable static mapper usage since it breaks unit tests (see https://github.com/aspnetboilerplate/aspnetboilerplate/issues/2052)
        Configuration.Modules.AbpAutoMapper().UseStaticMapper = false;
#pragma warning restore CS0618 // Type or member is obsolete

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

        // Use database for language management
        Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

        RegisterFakeService<AbpZeroDbMigrator<AbpProjectNameDbContext>>();

        Configuration.ReplaceService<IEmailSender, NullEmailSender>(DependencyLifeStyle.Transient);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Initialize() =>
        ServiceCollectionRegistrar.Register(IocManager);

    private void RegisterFakeService<TService>()
        where TService : class =>
        IocManager.IocContainer.Register(
            Component.For<TService>()
            .UsingFactoryMethod(() => Substitute.For<TService>())
            .LifestyleSingleton());
}
