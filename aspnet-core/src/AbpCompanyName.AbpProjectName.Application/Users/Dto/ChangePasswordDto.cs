#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

using System.ComponentModel.DataAnnotations;

namespace AbpCompanyName.AbpProjectName.Users.Dto;

public class ChangePasswordDto
{
    [Required]
    public string CurrentPassword { get; set; }

    [Required]
    public string NewPassword { get; set; }
}
