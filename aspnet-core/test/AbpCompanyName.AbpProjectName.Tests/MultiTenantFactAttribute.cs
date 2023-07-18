#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

using Xunit;

namespace AbpCompanyName.AbpProjectName.Tests
{
    public sealed class MultiTenantFactAttribute : FactAttribute
    {
        public MultiTenantFactAttribute()
        {
            if (!AbpProjectNameConsts.MultiTenancyEnabled)
            {
                Skip = "MultiTenancy is disabled.";
            }
        }
    }
}
