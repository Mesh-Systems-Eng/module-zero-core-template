#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Auditing;
using System.ComponentModel.DataAnnotations;

namespace AbpCompanyName.AbpProjectName.Web.Models.Account;

public class LoginViewModel
{
    [Required]
    public string UsernameOrEmailAddress { get; set; }

    [Required]
    [DisableAuditing]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
