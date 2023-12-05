#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Authentication.External;

public interface IExternalAuthProviderApi
{
    ExternalLoginProviderInfo ProviderInfo { get; }

    Task<bool> IsValidUser(string userId, string accessCode);

    Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);

    void Initialize(ExternalLoginProviderInfo providerInfo);
}
