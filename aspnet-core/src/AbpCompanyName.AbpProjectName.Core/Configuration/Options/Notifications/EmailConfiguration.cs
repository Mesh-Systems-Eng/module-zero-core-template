// <copyright file="EmailConfiguration.cs" company="Mesh Systems LLC">
// Copyright © 2023 Mesh Systems LLC.
// </copyright>

using Abp.Net.Mail;
using System.Net;

namespace AbpCompanyName.AbpProjectName.Configuration.Options.Notifications;

public class EmailConfiguration : IEmailSenderConfiguration, ISubscriptionConfiguration
{
    public HttpStatusCode[] SuccessfulStatusCodes { get; } = new HttpStatusCode[] { HttpStatusCode.Accepted, HttpStatusCode.OK };

    public string DefaultFromAddress { get; init; }

    public string DefaultFromDisplayName { get; init; }

    public string SubscriptionId { get; init; }

    public string Category { get; init; }
}
