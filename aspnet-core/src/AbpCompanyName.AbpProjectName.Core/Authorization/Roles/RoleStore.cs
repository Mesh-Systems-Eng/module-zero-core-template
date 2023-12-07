#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Authorization.Roles;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using AbpCompanyName.AbpProjectName.Authorization.Users;

namespace AbpCompanyName.AbpProjectName.Authorization.Roles;

public class RoleStore(
    IUnitOfWorkManager unitOfWorkManager,
    IRepository<Role> roleRepository,
    IRepository<RolePermissionSetting, long> rolePermissionSettingRepository) : AbpRoleStore<Role, User>(
        unitOfWorkManager,
        roleRepository,
        rolePermissionSettingRepository)
{
}
