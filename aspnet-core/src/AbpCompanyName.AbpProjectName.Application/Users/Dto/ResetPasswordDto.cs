#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using System.ComponentModel.DataAnnotations;

namespace AbpCompanyName.AbpProjectName.Users.Dto;

public class ResetPasswordDto
{
    [Required]
    public string AdminPassword { get; set; }

    [Required]
    public long UserId { get; set; }

    [Required]
    public string NewPassword { get; set; }
}
