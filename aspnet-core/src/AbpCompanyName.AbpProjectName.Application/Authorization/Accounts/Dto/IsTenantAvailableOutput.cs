#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

namespace AbpCompanyName.AbpProjectName.Authorization.Accounts.Dto;

public class IsTenantAvailableOutput
{
    public IsTenantAvailableOutput()
    {
    }

    public IsTenantAvailableOutput(TenantAvailabilityState state, int? tenantId = null)
    {
        State = state;
        TenantId = tenantId;
    }

    public TenantAvailabilityState State { get; set; }

    public int? TenantId { get; set; }
}
