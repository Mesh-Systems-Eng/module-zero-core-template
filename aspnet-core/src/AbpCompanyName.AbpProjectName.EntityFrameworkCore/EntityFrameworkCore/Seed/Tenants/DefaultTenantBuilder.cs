#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.MultiTenancy;
using AbpCompanyName.AbpProjectName.Editions;
using AbpCompanyName.AbpProjectName.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AbpCompanyName.AbpProjectName.EntityFrameworkCore.Seed.Tenants;

public class DefaultTenantBuilder(AbpProjectNameDbContext context)
{
    private readonly AbpProjectNameDbContext _context = context;

    public void Create() =>
        CreateDefaultTenant();

    private void CreateDefaultTenant()
    {
        // Default tenant

        var defaultTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == AbpTenantBase.DefaultTenantName);
        if (defaultTenant == null)
        {
            defaultTenant = new Tenant(AbpTenantBase.DefaultTenantName, AbpTenantBase.DefaultTenantName);

            var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                defaultTenant.EditionId = defaultEdition.Id;
            }

            _context.Tenants.Add(defaultTenant);
            _context.SaveChanges();
        }
    }
}
