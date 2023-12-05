#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Extensions;
using System.Text.RegularExpressions;

namespace AbpCompanyName.AbpProjectName.Validation;

public static partial class ValidationHelper
{
    public const string EmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    public static bool IsEmail(string value) => !value.IsNullOrEmpty() && GeneratedEmailRegex().IsMatch(value);

    [GeneratedRegex(EmailRegex)]
    private static partial Regex GeneratedEmailRegex();
}
