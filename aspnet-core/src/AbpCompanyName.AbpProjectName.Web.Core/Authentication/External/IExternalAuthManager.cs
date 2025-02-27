﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Authentication.External;

public interface IExternalAuthManager
{
    Task<bool> IsValidUser(string provider, string providerKey, string providerAccessCode);

    Task<ExternalAuthUserInfo> GetUserInfo(string provider, string accessCode);
}
