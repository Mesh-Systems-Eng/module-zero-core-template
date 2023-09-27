// <copyright file="SmsService.cs" company="Mesh Systems LLC">
// Copyright © 2023 Mesh Systems LLC.
// </copyright>

using Abp.Application.Services;
using AbpCompanyName.AbpProjectName.Configuration.Options;
using AbpCompanyName.AbpProjectName.Configuration.Options.Notifications;
using Mesh.Shared.Authorization;
using Mesh.Shared.Notifications;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Notifications.Sms;

[RemoteService(IsEnabled = false, IsMetadataEnabled = false)]
public class SmsService : ISmsService
{
    private const string AuthTokenCacheName = "SmsNotifications";
    private static readonly HttpStatusCode[] _successfulStatusCodes = new HttpStatusCode[] { HttpStatusCode.Accepted, HttpStatusCode.OK };
    private readonly SmsConfiguration _smsConfiguration;

    public SmsService(IOptions<SmsConfiguration> smsConfiguration, IOptions<SharedServicesConfiguration> sharedServiceConfiguration)
    {
        var authTokenCache = new AuthTokenCache();
        authTokenCache.RegisterCredentials(AuthTokenCacheName, sharedServiceConfiguration.Value.Credentials);
        SendText.SetAuthTokenCache(authTokenCache, AuthTokenCacheName);

        _smsConfiguration = smsConfiguration.Value;
    }

    public async Task SendTextAsync(SmsInput smsInput)
    {
        var message = new SendText(_smsConfiguration.SubscriptionId)
        {
            To = smsInput.PhoneNumber,
            Text = smsInput.Text
        };

        var result = await message.SendTextAsync();

        if (_successfulStatusCodes.Contains(result.StatusCode))
        {
            return;
        }

        throw result.Exception ?? new WebException($"An error occurred while trying to send the text. {result.Content}");
    }
}
