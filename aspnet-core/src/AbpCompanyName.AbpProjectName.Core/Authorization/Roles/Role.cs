#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Authorization.Roles;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using System.ComponentModel.DataAnnotations;

namespace AbpCompanyName.AbpProjectName.Authorization.Roles;

public class Role : AbpRole<User>
{
    public const int MaxDescriptionLength = 5000;

    public Role()
    {
    }

    public Role(int? tenantId, string displayName)
        : base(tenantId, displayName)
    {
    }

    public Role(int? tenantId, string name, string displayName)
        : base(tenantId, name, displayName)
    {
    }

    [StringLength(MaxDescriptionLength)]
    public string Description { get; set; }
}
