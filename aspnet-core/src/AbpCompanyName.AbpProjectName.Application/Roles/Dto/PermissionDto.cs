#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;

namespace AbpCompanyName.AbpProjectName.Roles.Dto;

[AutoMapFrom(typeof(Permission))]
public class PermissionDto : EntityDto<long>
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string Description { get; set; }
}
