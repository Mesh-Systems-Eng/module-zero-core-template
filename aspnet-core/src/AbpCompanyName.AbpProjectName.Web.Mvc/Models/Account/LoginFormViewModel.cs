#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

using Abp.MultiTenancy;

namespace AbpCompanyName.AbpProjectName.Web.Models.Account;

public class LoginFormViewModel
{
    public string ReturnUrl { get; set; }

    public bool IsMultiTenancyEnabled { get; set; }

    public bool IsSelfRegistrationAllowed { get; set; }

    public MultiTenancySides MultiTenancySide { get; set; }
}
