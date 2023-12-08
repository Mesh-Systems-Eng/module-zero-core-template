#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Dependency;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Authentication.External;

public abstract class ExternalAuthProviderApiBase : IExternalAuthProviderApi, ITransientDependency
{
    public ExternalLoginProviderInfo ProviderInfo { get; set; }

    public void Initialize(ExternalLoginProviderInfo providerInfo) =>
        ProviderInfo = providerInfo;

    public async Task<bool> IsValidUser(string userId, string accessCode)
    {
        var userInfo = await GetUserInfo(accessCode);
        return userInfo.ProviderKey == userId;
    }

    public abstract Task<ExternalAuthUserInfo> GetUserInfo(string accessCode);
}
