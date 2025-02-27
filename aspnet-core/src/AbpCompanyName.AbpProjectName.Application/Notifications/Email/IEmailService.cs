﻿// <copyright file="IEmailService.cs" company="Mesh Systems LLC">
// Copyright © 2023 Mesh Systems LLC.
// </copyright>

using Abp.Application.Services;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Notifications.Email;

[RemoteService(IsEnabled = false, IsMetadataEnabled = false)]
public interface IEmailService : IApplicationService
{
    Task SendEmailAsync(EmailInput emailInput);
}
