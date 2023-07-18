#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

using System.Collections.Generic;

namespace AbpCompanyName.AbpProjectName.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
