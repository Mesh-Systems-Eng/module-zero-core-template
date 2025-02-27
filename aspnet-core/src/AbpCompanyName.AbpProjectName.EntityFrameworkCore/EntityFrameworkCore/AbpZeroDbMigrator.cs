﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.MultiTenancy;
using Abp.Zero.EntityFrameworkCore;

namespace AbpCompanyName.AbpProjectName.EntityFrameworkCore;

public class AbpZeroDbMigrator(
    IUnitOfWorkManager unitOfWorkManager,
    IDbPerTenantConnectionStringResolver connectionStringResolver,
    IDbContextResolver dbContextResolver) : AbpZeroDbMigrator<AbpProjectNameDbContext>(
        unitOfWorkManager,
        connectionStringResolver,
        dbContextResolver)
{
}
