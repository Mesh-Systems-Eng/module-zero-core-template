// <copyright file="ServiceCollectionExtension.cs" company="Mesh Systems LLC">
// Copyright © 2023 Mesh Systems LLC.
// </copyright>

using AbpCompanyName.AbpProjectName.Configuration.Options.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AbpCompanyName.AbpProjectName.Configuration.Options
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterSharedServicesConfiguration(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<SharedServicesConfiguration>(configuration.GetSection("SharedServicesConfiguration"));

        public static IServiceCollection RegisterEmailConfiguration(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<EmailConfiguration>(configuration.GetSection("Notifications:EmailConfiguration"));

        public static IServiceCollection RegisterSmsConfiguration(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<SmsConfiguration>(configuration.GetSection("Notifications:SmsConfiguration"));
    }
}
