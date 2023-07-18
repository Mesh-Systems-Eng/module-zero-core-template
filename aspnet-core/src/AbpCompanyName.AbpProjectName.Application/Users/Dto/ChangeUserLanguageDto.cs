#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

using System.ComponentModel.DataAnnotations;

namespace AbpCompanyName.AbpProjectName.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}