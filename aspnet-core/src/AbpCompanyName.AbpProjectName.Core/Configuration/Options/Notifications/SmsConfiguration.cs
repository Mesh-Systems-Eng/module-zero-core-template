// <copyright file="SmsConfiguration.cs" company="Mesh Systems LLC">
// Copyright © 2023 Mesh Systems LLC.
// </copyright>

using System.Net;

namespace AbpCompanyName.AbpProjectName.Configuration.Options.Notifications;

public class SmsConfiguration : ISubscriptionConfiguration
{
    public HttpStatusCode[] SuccessfulStatusCodes { get; } = new HttpStatusCode[] { HttpStatusCode.Accepted, HttpStatusCode.OK };

    public string SubscriptionId { get; init; }
}
