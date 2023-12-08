#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Web.Security.AntiForgery;
using AbpCompanyName.AbpProjectName.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace AbpCompanyName.AbpProjectName.Web.Host.Controllers;

public class AntiForgeryController(IAntiforgery antiforgery, IAbpAntiForgeryManager antiForgeryManager) : AbpProjectNameControllerBase
{
    private readonly IAntiforgery _antiforgery = antiforgery;
    private readonly IAbpAntiForgeryManager _antiForgeryManager = antiForgeryManager;

    public void GetToken() =>
        _antiforgery.SetCookieTokenAndHeader(HttpContext);

    public void SetCookie() =>
        _antiForgeryManager.SetCookie(HttpContext);
}
