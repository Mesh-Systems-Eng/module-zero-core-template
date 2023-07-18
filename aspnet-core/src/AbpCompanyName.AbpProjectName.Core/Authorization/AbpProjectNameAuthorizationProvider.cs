#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace AbpCompanyName.AbpProjectName.Authorization
{
    public class AbpProjectNameAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.PagesUsers, L("Users"));
            context.CreatePermission(PermissionNames.PagesUsersActivation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.PagesRoles, L("Roles"));
            context.CreatePermission(PermissionNames.PagesTenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name) => new LocalizableString(name, AbpProjectNameConsts.LocalizationSourceName);
    }
}
