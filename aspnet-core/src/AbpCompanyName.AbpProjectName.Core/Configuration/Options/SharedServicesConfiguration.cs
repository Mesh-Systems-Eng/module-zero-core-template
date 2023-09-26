// <copyright file="SharedServicesConfiguration.cs" company="Mesh Systems LLC">
// Copyright © 2023 Mesh Systems LLC.
// </copyright>

using Mesh.Shared.Authorization;

namespace AbpCompanyName.AbpProjectName.Configuration.Options;

public class SharedServicesConfiguration
{
    public string ClientId { get; init; }

    public string ClientSecret { get; init; }

    public string AzureTenant { get; init; }

    public ClientCredentials Credentials => new ClientCredentials
    {
        ClientId = ClientId,
        ClientSecret = ClientSecret,
        AzureTenant = string.IsNullOrWhiteSpace(AzureTenant) ? null : AzureTenant
    };
}
