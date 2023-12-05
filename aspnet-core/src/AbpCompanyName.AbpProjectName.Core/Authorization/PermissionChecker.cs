﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

using Abp.Authorization;
using AbpCompanyName.AbpProjectName.Authorization.Roles;
using AbpCompanyName.AbpProjectName.Authorization.Users;

namespace AbpCompanyName.AbpProjectName.Authorization;

public class PermissionChecker(UserManager userManager) : PermissionChecker<Role, User>(userManager)
{
}
