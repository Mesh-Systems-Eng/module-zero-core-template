#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace AbpCompanyName.AbpProjectName.Web.Views
{
    public abstract class AbpProjectNameRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected AbpProjectNameRazorPage() => LocalizationSourceName = AbpProjectNameConsts.LocalizationSourceName;

        [RazorInject]
        public IAbpSession AbpSession { get; set; }
    }
}
