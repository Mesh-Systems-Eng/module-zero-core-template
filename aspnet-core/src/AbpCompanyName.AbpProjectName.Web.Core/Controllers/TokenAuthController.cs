#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using AbpCompanyName.AbpProjectName.Authentication.JwtBearer;
using AbpCompanyName.AbpProjectName.Authorization;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.Models.TokenAuth;
using AbpCompanyName.AbpProjectName.MultiTenancy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Controllers;

[Route("api/[controller]/[action]")]
public class TokenAuthController(
    LogInManager logInManager,
    ITenantCache tenantCache,
    AbpLoginResultTypeHelper abpLoginResultTypeHelper,
    TokenAuthConfiguration configuration) : AbpProjectNameControllerBase
{
    private readonly LogInManager _logInManager = logInManager;
    private readonly ITenantCache _tenantCache = tenantCache;
    private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
    private readonly TokenAuthConfiguration _configuration = configuration;

    [HttpPost]
    public async Task<AuthenticateResultModel> Authenticate([FromBody] AuthenticateModel model)
    {
        var loginResult = await GetLoginResultAsync(
            model.UserNameOrEmailAddress,
            model.Password,
            GetTenancyNameOrNull());

        var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

        return new AuthenticateResultModel
        {
            AccessToken = accessToken,
            EncryptedAccessToken = GetEncryptedAccessToken(accessToken),
            ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
            UserId = loginResult.User.Id
        };
    }

    private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
    {
        var claims = identity.Claims.ToList();
        var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

        // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
        claims.AddRange(new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            #pragma warning disable CA1305 // Specify IFormatProvider
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            #pragma warning restore CA1305 // Specify IFormatProvider
        });

        return claims;
    }

    private static string GetEncryptedAccessToken(string accessToken) =>
        SimpleStringCipher.Instance.Encrypt(accessToken);

    private string GetTenancyNameOrNull() =>
        !AbpSession.TenantId.HasValue
        ? null
        : _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;

    private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
    {
        var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

        switch (loginResult.Result)
        {
            case AbpLoginResultType.Success:
                return loginResult;
            case AbpLoginResultType.InvalidUserNameOrEmailAddress:
            case AbpLoginResultType.InvalidPassword:
            case AbpLoginResultType.UserIsNotActive:
            case AbpLoginResultType.InvalidTenancyName:
            case AbpLoginResultType.TenantIsNotActive:
            case AbpLoginResultType.UserEmailIsNotConfirmed:
            case AbpLoginResultType.UnknownExternalLogin:
            case AbpLoginResultType.LockedOut:
            case AbpLoginResultType.UserPhoneNumberIsNotConfirmed:
            case AbpLoginResultType.FailedForOtherReason:
            default:
                throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
        }
    }

    private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
    {
        var now = DateTime.UtcNow;

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            claims: claims,
            notBefore: now,
            expires: now.Add(expiration ?? _configuration.Expiration),
            signingCredentials: _configuration.SigningCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
