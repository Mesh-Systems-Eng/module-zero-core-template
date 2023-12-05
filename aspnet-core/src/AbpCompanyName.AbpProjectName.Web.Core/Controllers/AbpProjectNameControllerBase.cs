#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace AbpCompanyName.AbpProjectName.Controllers;

public abstract class AbpProjectNameControllerBase : AbpController
{
    protected AbpProjectNameControllerBase() =>
        LocalizationSourceName = AbpProjectNameConsts.LocalizationSourceName;

    protected void CheckErrors(IdentityResult identityResult) =>
        identityResult.CheckErrors(LocalizationManager);
}
