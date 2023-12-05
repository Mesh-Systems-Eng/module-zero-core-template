#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using AbpCompanyName.AbpProjectName.Roles.Dto;
using AbpCompanyName.AbpProjectName.Users.Dto;
using System.Collections.Generic;
using System.Linq;

namespace AbpCompanyName.AbpProjectName.Web.Models.Users;

public class EditUserModalViewModel
{
    public UserDto User { get; set; }

    public IReadOnlyList<RoleDto> Roles { get; set; }

    public bool UserIsInRole(RoleDto role) =>
        User.RoleNames != null && User.RoleNames.Any(r => r == role.NormalizedName);
}
