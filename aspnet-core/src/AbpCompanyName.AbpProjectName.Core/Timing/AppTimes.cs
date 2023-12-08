#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Dependency;
using System;

namespace AbpCompanyName.AbpProjectName.Timing;

public class AppTimes : ISingletonDependency
{
    /// <summary>
    /// Gets or sets the startup time of the application.
    /// </summary>
    public DateTime StartupTime { get; set; }
}
