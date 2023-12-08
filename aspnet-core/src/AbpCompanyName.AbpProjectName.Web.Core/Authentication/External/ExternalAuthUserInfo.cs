#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

namespace AbpCompanyName.AbpProjectName.Authentication.External;

public class ExternalAuthUserInfo
{
    public string ProviderKey { get; set; }

    public string Name { get; set; }

    public string EmailAddress { get; set; }

    public string Surname { get; set; }

    public string Provider { get; set; }
}
