#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

#pragma warning disable CA2201 // Do not raise reserved exception types

using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class AbpProjectNameAppServiceBase : ApplicationService
    {
        protected AbpProjectNameAppServiceBase() =>
            LocalizationSourceName = AbpProjectNameConsts.LocalizationSourceName;

        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected virtual async Task<User> GetCurrentUserAsync() =>
            await UserManager.FindByIdAsync($"{AbpSession.GetUserId()}")
            ?? throw new Exception("There is no current user!");

        protected virtual Task<Tenant> GetCurrentTenantAsync() =>
            TenantManager.GetByIdAsync(AbpSession.GetTenantId());

        protected virtual void CheckErrors(IdentityResult identityResult) =>
            identityResult.CheckErrors(LocalizationManager);
    }
}
