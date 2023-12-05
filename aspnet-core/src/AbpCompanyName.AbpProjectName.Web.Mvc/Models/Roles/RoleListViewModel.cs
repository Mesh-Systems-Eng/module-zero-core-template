#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using AbpCompanyName.AbpProjectName.Roles.Dto;
using System.Collections.Generic;

namespace AbpCompanyName.AbpProjectName.Web.Models.Roles;

public class RoleListViewModel
{
    public IReadOnlyList<PermissionDto> Permissions { get; set; }
}
