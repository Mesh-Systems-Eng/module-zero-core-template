#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Configuration;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AbpCompanyName.AbpProjectName.EntityFrameworkCore.Seed.Host;

public class DefaultSettingsCreator(AbpProjectNameDbContext context)
{
    private readonly AbpProjectNameDbContext _context = context;

    public void Create()
    {
        int? tenantId = AbpProjectNameConsts.MultiTenancyEnabled
            ? null
            : MultiTenancyConsts.DefaultTenantId;

        // Emailing
        AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "admin@mydomain.com", tenantId);
        AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "mydomain.com mailer", tenantId);

        // Languages
        AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en", tenantId);
    }

    private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
    {
        if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
        {
            return;
        }

        _context.Settings.Add(new Setting(tenantId, null, name, value));
        _context.SaveChanges();
    }
}
