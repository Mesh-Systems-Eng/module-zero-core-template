#pragma warning disable IDE0073
// Copyright � 2016 ASP.NET Boilerplate
// Contributions Copyright � 2023 Mesh Systems LLC

using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace AbpCompanyName.AbpProjectName.MultiTenancy.Dto;

[AutoMapTo(typeof(Tenant))]
public class CreateTenantDto
{
    [Required]
    [StringLength(AbpTenantBase.MaxTenancyNameLength)]
    [RegularExpression(AbpTenantBase.TenancyNameRegex)]
    public string TenancyName { get; set; }

    [Required]
    [StringLength(AbpTenantBase.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    [StringLength(AbpUserBase.MaxEmailAddressLength)]
    public string AdminEmailAddress { get; set; }

    [StringLength(AbpTenantBase.MaxConnectionStringLength)]
    public string ConnectionString { get; set; }

    public bool IsActive { get; set; }
}
