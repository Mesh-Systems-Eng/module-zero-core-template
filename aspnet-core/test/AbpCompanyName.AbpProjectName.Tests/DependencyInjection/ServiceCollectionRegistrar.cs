﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Dependency;
using AbpCompanyName.AbpProjectName.EntityFrameworkCore;
using AbpCompanyName.AbpProjectName.Identity;
using Castle.MicroKernel.Registration;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AbpCompanyName.AbpProjectName.Tests.DependencyInjection;

public static class ServiceCollectionRegistrar
{
    public static void Register(IIocManager iocManager)
    {
        var services = new ServiceCollection();

        IdentityRegistrar.Register(services);

        services.AddEntityFrameworkInMemoryDatabase();

        var serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(iocManager.IocContainer, services);

        var builder = new DbContextOptionsBuilder<AbpProjectNameDbContext>();
        builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseInternalServiceProvider(serviceProvider);

        iocManager.IocContainer.Register(
            Component
                .For<DbContextOptions<AbpProjectNameDbContext>>()
                .Instance(builder.Options)
                .LifestyleSingleton());
    }
}
