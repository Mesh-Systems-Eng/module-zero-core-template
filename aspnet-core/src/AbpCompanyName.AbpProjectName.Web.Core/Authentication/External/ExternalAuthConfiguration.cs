#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Dependency;
using System.Collections.Generic;

namespace AbpCompanyName.AbpProjectName.Authentication.External;

public class ExternalAuthConfiguration : IExternalAuthConfiguration, ISingletonDependency
{
    public ExternalAuthConfiguration() =>
        Providers = [];

    public List<ExternalLoginProviderInfo> Providers { get; }
}
