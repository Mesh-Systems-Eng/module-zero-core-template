#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Authorization;
using Abp.Domain.Uow;
using AbpCompanyName.AbpProjectName.Authorization.Roles;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.MultiTenancy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AbpCompanyName.AbpProjectName.Identity;

#pragma warning disable CS0618 // Type or member is obsolete
public class SecurityStampValidator(
    IOptions<SecurityStampValidatorOptions> options,
    SignInManager signInManager,
    ISystemClock systemClock,
    ILoggerFactory loggerFactory,
    IUnitOfWorkManager unitOfWorkManager) : AbpSecurityStampValidator<Tenant, Role, User>(options, signInManager, systemClock, loggerFactory, unitOfWorkManager)
{
}
#pragma warning restore CS0618 // Type or member is obsolete
