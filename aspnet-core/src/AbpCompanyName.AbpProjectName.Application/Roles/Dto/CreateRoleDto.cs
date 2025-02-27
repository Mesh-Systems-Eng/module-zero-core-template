#pragma warning disable IDE0073
// Copyright � 2016 ASP.NET Boilerplate
// Contributions Copyright � 2023 Mesh Systems LLC

using Abp.Authorization.Roles;
using AbpCompanyName.AbpProjectName.Authorization.Roles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AbpCompanyName.AbpProjectName.Roles.Dto;

public class CreateRoleDto
{
    public CreateRoleDto() =>
        GrantedPermissions = [];

    [Required]
    [StringLength(AbpRoleBase.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    [StringLength(AbpRoleBase.MaxDisplayNameLength)]
    public string DisplayName { get; set; }

    public string NormalizedName { get; set; }

    [StringLength(Role.MaxDescriptionLength)]
    public string Description { get; set; }

    public List<string> GrantedPermissions { get; set; }
}
