﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Authorization.Users;
using Abp.Domain.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using AbpCompanyName.AbpProjectName.Authorization.Roles;
using AbpCompanyName.AbpProjectName.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Authorization.Users;

public class UserRegistrationManager(
    TenantManager tenantManager,
    UserManager userManager,
    RoleManager roleManager,
    IPasswordHasher<User> passwordHasher) : DomainService
{
    private readonly TenantManager _tenantManager = tenantManager;
    private readonly UserManager _userManager = userManager;
    private readonly RoleManager _roleManager = roleManager;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Initial framework.")]
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

    public IAbpSession AbpSession { get; set; } = NullAbpSession.Instance;

    public async Task<User> RegisterAsync(string name, string surname, string emailAddress, string userName, string plainPassword, bool isEmailConfirmed)
    {
        CheckForTenant();

        var tenant = await GetActiveTenantAsync();

        var user = new User
        {
            TenantId = tenant.Id,
            Name = name,
            Surname = surname,
            EmailAddress = emailAddress,
            IsActive = true,
            UserName = userName,
            IsEmailConfirmed = isEmailConfirmed,
            Roles = new List<UserRole>()
        };

        user.SetNormalizedNames();

        foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
        {
            user.Roles.Add(new UserRole(tenant.Id, user.Id, defaultRole.Id));
        }

        await _userManager.InitializeOptionsAsync(tenant.Id);

        CheckErrors(await _userManager.CreateAsync(user, plainPassword));
        await CurrentUnitOfWork.SaveChangesAsync();

        return user;
    }

    protected virtual void CheckErrors(IdentityResult identityResult) =>
        identityResult.CheckErrors(LocalizationManager);

    private void CheckForTenant()
    {
        if (!AbpSession.TenantId.HasValue)
        {
            throw new InvalidOperationException("Can not register host users!");
        }
    }

    private async Task<Tenant> GetActiveTenantAsync() =>
        !AbpSession.TenantId.HasValue
        ? null
        : await GetActiveTenantAsync(AbpSession.TenantId.Value);

    private async Task<Tenant> GetActiveTenantAsync(int tenantId)
    {
        var tenant = await _tenantManager.FindByIdAsync(tenantId)
            ?? throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));

        return !tenant.IsActive
            ? throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId))
            : tenant;
    }
}
