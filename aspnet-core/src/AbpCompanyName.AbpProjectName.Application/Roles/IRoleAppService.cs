#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AbpCompanyName.AbpProjectName.Roles.Dto;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Roles;

public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>
{
    Task<ListResultDto<PermissionDto>> GetAllPermissions();

    Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input);

    Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);
}
