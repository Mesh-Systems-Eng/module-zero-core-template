﻿// <copyright file="ISmsService.cs" company="Mesh Systems LLC">
// Copyright © 2023 Mesh Systems LLC.
// </copyright>

using Abp.Application.Services;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Notifications.Sms;

public interface ISmsService : IApplicationService
{
    Task SendTextAsync(SmsInput smsInput);
}
