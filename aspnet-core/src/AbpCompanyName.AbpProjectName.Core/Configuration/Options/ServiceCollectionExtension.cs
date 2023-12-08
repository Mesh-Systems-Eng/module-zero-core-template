// <copyright file="ServiceCollectionExtension.cs" company="Mesh Systems LLC">
// Copyright © 2023 Mesh Systems LLC.
// </copyright>

using AbpCompanyName.AbpProjectName.Configuration.Options.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AbpCompanyName.AbpProjectName.Configuration.Options;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterSharedServicesConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<SharedServicesConfiguration>(configuration.GetSection("SharedServicesConfiguration"));

    public static IServiceCollection RegisterEmailConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<EmailConfiguration>(configuration.GetSection("Notifications:EmailConfiguration"));

    public static IServiceCollection RegisterSmsConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<SmsConfiguration>(configuration.GetSection("Notifications:SmsConfiguration"));

    public static IServiceCollection ConfigureBackgroundWorkerTimer(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            // A timer is set (in milliseconds) in the background to check for jobs to run by default every 5 seconds.
            // Disabling this timer would cause the UserTokenExpirationWorker to not run.
            var backgroundPollingPeriodInSeconds = configuration.GetValue<int>("BackgroundPollingPeriodInSeconds");
            var backgroundPollingPeriodInMilliseconds = backgroundPollingPeriodInSeconds <= 0 ? 5000 : backgroundPollingPeriodInSeconds * 1000;
            Abp.BackgroundJobs.BackgroundJobManager.JobPollPeriod = backgroundPollingPeriodInMilliseconds;
        }
        catch (Exception)
        {
            // intentionally ignoring on failure.
        }

        return services;
    }
}
