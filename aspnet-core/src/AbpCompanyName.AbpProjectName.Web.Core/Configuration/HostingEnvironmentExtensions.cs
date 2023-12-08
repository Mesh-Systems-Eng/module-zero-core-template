#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AbpCompanyName.AbpProjectName.Configuration;

public static class HostingEnvironmentExtensions
{
    public static IConfigurationRoot GetAppConfiguration(this IWebHostEnvironment env) =>
        AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
}
