#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.AspNetCore.Mvc.ViewComponents;

namespace AbpCompanyName.AbpProjectName.Web.Views
{
    public abstract class AbpProjectNameViewComponent : AbpViewComponent
    {
        protected AbpProjectNameViewComponent() => LocalizationSourceName = AbpProjectNameConsts.LocalizationSourceName;
    }
}
