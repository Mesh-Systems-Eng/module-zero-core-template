#pragma warning disable IDE0073
// Copyright � 2016 ASP.NET Boilerplate
// Contributions Copyright � 2023 Mesh Systems LLC

using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using AbpCompanyName.AbpProjectName.Authorization.Roles;

namespace AbpCompanyName.AbpProjectName.Authorization.Users;

public class UserStore(
    IUnitOfWorkManager unitOfWorkManager,
    IRepository<User, long> userRepository,
    IRepository<Role> roleRepository,
    IRepository<UserRole, long> userRoleRepository,
    IRepository<UserLogin, long> userLoginRepository,
    IRepository<UserClaim, long> userClaimRepository,
    IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
    IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
    IRepository<OrganizationUnitRole, long> organizationUnitRoleRepository,
    IRepository<UserToken, long> userTokenRepository) : AbpUserStore<Role, User>(
        unitOfWorkManager,
        userRepository,
        roleRepository,
        userRoleRepository,
        userLoginRepository,
        userClaimRepository,
        userPermissionSettingRepository,
        userOrganizationUnitRepository,
        organizationUnitRoleRepository,
        userTokenRepository)
{
}
