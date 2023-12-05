#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using System.Collections.Generic;

namespace AbpCompanyName.AbpProjectName.Authentication.External;

public interface IExternalAuthConfiguration
{
    List<ExternalLoginProviderInfo> Providers { get; }
}
