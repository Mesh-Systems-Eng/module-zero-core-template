#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Application.Services.Dto;

namespace AbpCompanyName.AbpProjectName.Users.Dto;

public class PagedUserResultRequestDto : PagedResultRequestDto
{
    public string Keyword { get; set; }
    public bool? IsActive { get; set; }
}
