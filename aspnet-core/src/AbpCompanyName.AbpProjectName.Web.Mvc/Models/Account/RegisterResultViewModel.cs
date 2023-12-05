#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

namespace AbpCompanyName.AbpProjectName.Web.Models.Account;

public class RegisterResultViewModel
{
    public string TenancyName { get; set; }

    public string UserName { get; set; }

    public string EmailAddress { get; set; }

    public string NameAndSurname { get; set; }

    public bool IsActive { get; set; }

    public bool IsEmailConfirmationRequiredForLogin { get; set; }

    public bool IsEmailConfirmed { get; set; }
}
