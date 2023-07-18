#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

using System.Collections.Generic;

namespace AbpCompanyName.AbpProjectName.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}