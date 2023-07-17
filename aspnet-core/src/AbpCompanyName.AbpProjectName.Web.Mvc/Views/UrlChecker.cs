#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using System.Text.RegularExpressions;

namespace AbpCompanyName.AbpProjectName.Web.Views
{
    public static partial class UrlChecker
    {
        public static bool IsRooted(string url) =>
            url.StartsWith("/", System.StringComparison.OrdinalIgnoreCase)
            || GeneratedUrlWithProtocolRegex().IsMatch(url);

        [GeneratedRegex("^.{1,10}://.*$")]
        private static partial Regex GeneratedUrlWithProtocolRegex();
    }
}
