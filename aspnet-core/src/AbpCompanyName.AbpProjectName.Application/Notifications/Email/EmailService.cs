// <copyright file="EmailService.cs" company="Mesh Systems LLC">
// Copyright © 2023 Mesh Systems LLC.
// </copyright>

using AbpCompanyName.AbpProjectName.Configuration.Options;
using AbpCompanyName.AbpProjectName.Configuration.Options.Notifications;
using Mesh.Shared.Authorization;
using Mesh.Shared.Notifications;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Notifications.Email;

public class EmailService : IEmailService
{
    private const string AuthTokenCacheName = "EmailNotifications";
    private readonly EmailConfiguration _emailConfiguration;

    public EmailService(IOptions<EmailConfiguration> emailConfiguration, IOptions<SharedServicesConfiguration> sharedServiceConfiguration)
    {
        var authTokenCache = new AuthTokenCache();
        authTokenCache.RegisterCredentials(AuthTokenCacheName, sharedServiceConfiguration.Value.Credentials);
        SendMessage.SetAuthTokenCache(authTokenCache, AuthTokenCacheName);

        _emailConfiguration = emailConfiguration.Value;
    }

    public async Task SendEmailAsync(EmailInput emailInput)
    {
        var message = new SendMessage(_emailConfiguration.SubscriptionId)
        {
            Subject = emailInput.Subject,
            From = new EmailAddress
            {
                Email = _emailConfiguration.DefaultFromAddress,
                Name = _emailConfiguration.DefaultFromDisplayName
            },
            ReplyTo = null
        };

        message.Categories.Add(_emailConfiguration.Category);

        message.SendToList.AddRange(emailInput.Addresses.Select(address => new EmailAddress { Email = address }));
        message.Attachments.AddRange(emailInput.Attachments);

        if (emailInput.IsHtml)
        {
            message.HtmlContent = emailInput.Body;
        }
        else
        {
            message.PlainTextContent = emailInput.Body;
        }

        var result = await message.SendMessageAsync();
        if (_emailConfiguration.SuccessfulStatusCodes.Contains(result.StatusCode))
        {
            return;
        }

        throw result.Exception ?? new WebException($"An error occurred while trying to send the email. {result.Content}");
    }
}
