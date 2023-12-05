#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.AspNetCore.Mvc.Authorization;
using AbpCompanyName.AbpProjectName.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AbpCompanyName.AbpProjectName.Web.Controllers;

[AbpMvcAuthorize]
public class AboutController : AbpProjectNameControllerBase
{
    public ActionResult Index() =>
        View();
}
