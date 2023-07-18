#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

namespace AbpCompanyName.AbpProjectName.Sessions.Dto
{
    public class GetCurrentLoginInformationsOutput
    {
        public ApplicationInfoDto Application { get; set; }

        public UserLoginInfoDto User { get; set; }

        public TenantLoginInfoDto Tenant { get; set; }
    }
}
