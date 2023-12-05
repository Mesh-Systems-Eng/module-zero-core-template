#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Authorization;
using Abp.Domain.Uow;
using AbpCompanyName.AbpProjectName.Authorization.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AbpCompanyName.AbpProjectName.Authorization.Users;

public class UserClaimsPrincipalFactory(
    UserManager userManager,
    RoleManager roleManager,
    IOptions<IdentityOptions> optionsAccessor,
    IUnitOfWorkManager unitOfWorkManager) : AbpUserClaimsPrincipalFactory<User, Role>(
          userManager,
          roleManager,
          optionsAccessor,
          unitOfWorkManager)
{
}
