﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using AbpCompanyName.AbpProjectName.Authorization.Roles;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.MultiTenancy;
using Microsoft.AspNetCore.Identity;

namespace AbpCompanyName.AbpProjectName.Authorization;

public class LogInManager(
    UserManager userManager,
    IMultiTenancyConfig multiTenancyConfig,
    IRepository<Tenant> tenantRepository,
    IUnitOfWorkManager unitOfWorkManager,
    ISettingManager settingManager,
    IRepository<UserLoginAttempt, long> userLoginAttemptRepository,
    IUserManagementConfig userManagementConfig,
    IIocResolver iocResolver,
    IPasswordHasher<User> passwordHasher,
    RoleManager roleManager,
    UserClaimsPrincipalFactory claimsPrincipalFactory) : AbpLogInManager<Tenant, Role, User>(
          userManager,
          multiTenancyConfig,
          tenantRepository,
          unitOfWorkManager,
          settingManager,
          userLoginAttemptRepository,
          userManagementConfig,
          iocResolver,
          passwordHasher,
          roleManager,
          claimsPrincipalFactory)
{
}
