#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

using System.ComponentModel.DataAnnotations;

namespace AbpCompanyName.AbpProjectName.Configuration.Dto;

public class ChangeUiThemeInput
{
    [Required]
    [StringLength(32)]
    public string Theme { get; set; }
}
