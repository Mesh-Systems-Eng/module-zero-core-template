#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Reflection.Extensions;
using System;
using System.IO;

namespace AbpCompanyName.AbpProjectName;

/// <summary>
/// Central point for application version.
/// </summary>
public class AppVersionHelper
{
    /// <summary>
    /// Gets current version of the application.
    /// It's also shown in the web page.
    /// </summary>
    public const string Version = "9.0.0.0";

    private static readonly Lazy<DateTime> _lazyReleaseDate = new Lazy<DateTime>(() => new FileInfo(typeof(AppVersionHelper).GetAssembly().Location).LastWriteTime);

    /// <summary>
    /// Gets release (last build) date of the application.
    /// It's shown in the web page.
    /// </summary>
    public static DateTime ReleaseDate => _lazyReleaseDate.Value;
}
