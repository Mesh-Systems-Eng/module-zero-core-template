#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

namespace AbpCompanyName.AbpProjectName.Configuration.Ui;

public class UiThemeInfo(string name, string cssClass)
{
    public string Name { get; } = name;
    public string CssClass { get; } = cssClass;
}
