#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

#pragma warning disable CA2201 // Do not raise reserved exception types

using Abp.Reflection.Extensions;
using System;
using System.IO;
using System.Linq;

namespace AbpCompanyName.AbpProjectName.Web;

/// <summary>
/// This class is used to find root path of the web project in;
/// unit tests (to find views) and entity framework core command line commands (to find conn string).
/// </summary>
public static class WebContentFolderHelper
{
    public static string CalculateContentRootFolder()
    {
        var coreAssemblyDirectoryPath =
            Path.GetDirectoryName(typeof(AbpProjectNameCoreModule).GetAssembly().Location)
            ?? throw new Exception("Could not find location of AbpCompanyName.AbpProjectName.Core assembly!");

        var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);
        while (!DirectoryContains(directoryInfo.FullName, "AbpCompanyName.AbpProjectName.sln"))
        {
            if (directoryInfo.Parent == null)
            {
                throw new Exception("Could not find content root folder!");
            }

            directoryInfo = directoryInfo.Parent;
        }

        var webMvcFolder = Path.Combine(directoryInfo.FullName, "src", "AbpCompanyName.AbpProjectName.Web.Mvc");
        if (Directory.Exists(webMvcFolder))
        {
            return webMvcFolder;
        }

        var webHostFolder = Path.Combine(directoryInfo.FullName, "src", "AbpCompanyName.AbpProjectName.Web.Host");

        return Directory.Exists(webHostFolder)
            ? webHostFolder
            : throw new Exception("Could not find root folder of the web project!");
    }

    private static bool DirectoryContains(string directory, string fileName) =>
        Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName, StringComparison.Ordinal));
}
