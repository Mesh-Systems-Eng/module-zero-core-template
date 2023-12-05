#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Configuration.Startup;
using AbpCompanyName.AbpProjectName.Sessions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Web.Views.Shared.Components.RightNavbarUserArea
{
    public class RightNavbarUserAreaViewComponent(
        ISessionAppService sessionAppService,
        IMultiTenancyConfig multiTenancyConfig) : AbpProjectNameViewComponent
    {
        private readonly ISessionAppService _sessionAppService = sessionAppService;
        private readonly IMultiTenancyConfig _multiTenancyConfig = multiTenancyConfig;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new RightNavbarUserAreaViewModel
            {
                LoginInformations = await _sessionAppService.GetCurrentLoginInformations(),
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
            };

            return View(model);
        }
    }
}
