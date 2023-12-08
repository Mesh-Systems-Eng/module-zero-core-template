#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using System;

namespace AbpCompanyName.AbpProjectName.Authentication.External;

public class ExternalLoginProviderInfo(string name, string clientId, string clientSecret, Type providerApiType)
{
    public string Name { get; set; } = name;

    public string ClientId { get; set; } = clientId;

    public string ClientSecret { get; set; } = clientSecret;

    public Type ProviderApiType { get; set; } = providerApiType;
}
