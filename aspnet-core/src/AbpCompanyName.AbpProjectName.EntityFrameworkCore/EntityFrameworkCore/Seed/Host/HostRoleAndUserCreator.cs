#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using AbpCompanyName.AbpProjectName.Authorization;
using AbpCompanyName.AbpProjectName.Authorization.Roles;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace AbpCompanyName.AbpProjectName.EntityFrameworkCore.Seed.Host;

public class HostRoleAndUserCreator(AbpProjectNameDbContext context)
{
    private readonly AbpProjectNameDbContext _context = context;

    public void Create() =>
        CreateHostRoleAndUsers();

    private void CreateHostRoleAndUsers()
    {
        // Admin role for host

        var adminRoleForHost = _context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
        if (adminRoleForHost == null)
        {
            adminRoleForHost = _context.Roles.Add(new Role(null, StaticRoleNames.Host.Admin, StaticRoleNames.Host.Admin) { IsStatic = true, IsDefault = true }).Entity;
            _context.SaveChanges();
        }

        // Grant all permissions to admin role for host

        var grantedPermissions = _context.Permissions.IgnoreQueryFilters()
            .OfType<RolePermissionSetting>()
            .Where(p => p.TenantId == null && p.RoleId == adminRoleForHost.Id)
            .Select(p => p.Name)
            .ToList();

        var permissions = PermissionFinder
            .GetAllPermissions(new AbpProjectNameAuthorizationProvider())
            .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host) &&
                        !grantedPermissions.Contains(p.Name))
            .ToList();

        if (permissions.Count != 0)
        {
            _context.Permissions.AddRange(
                permissions.Select(permission => new RolePermissionSetting
                {
                    TenantId = null,
                    Name = permission.Name,
                    IsGranted = true,
                    RoleId = adminRoleForHost.Id
                }));

            _context.SaveChanges();
        }

        // Admin user for host

        var adminUserForHost = _context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.TenantId == null && u.UserName == AbpUserBase.AdminUserName);
        if (adminUserForHost == null)
        {
            var user = new User
            {
                TenantId = null,
                UserName = AbpUserBase.AdminUserName,
                Name = "admin",
                Surname = "admin",
                EmailAddress = "admin@aspnetboilerplate.com",
                IsEmailConfirmed = true,
                IsActive = true
            };

            user.Password = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(new PasswordHasherOptions())).HashPassword(user, User.DefaultPassword);
            user.SetNormalizedNames();

            adminUserForHost = _context.Users.Add(user).Entity;
            _context.SaveChanges();

            // Assign Admin role to admin user
            _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));
            _context.SaveChanges();

            _context.SaveChanges();
        }
    }
}
