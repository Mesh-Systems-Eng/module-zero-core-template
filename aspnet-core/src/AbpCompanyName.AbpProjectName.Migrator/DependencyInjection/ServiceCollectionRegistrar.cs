#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Dependency;
using AbpCompanyName.AbpProjectName.Identity;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace AbpCompanyName.AbpProjectName.Migrator.DependencyInjection;

public static class ServiceCollectionRegistrar
{
    public static void Register(IIocManager iocManager)
    {
        var services = new ServiceCollection();

        IdentityRegistrar.Register(services);

        WindsorRegistrationHelper.CreateServiceProvider(iocManager.IocContainer, services);
    }
}
