#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.ObjectMapping;
using AbpCompanyName.AbpProjectName.Sessions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Web.Views.Shared.Components.TenantChange
{
    public class TenantChangeViewComponent(ISessionAppService sessionAppService, IObjectMapper objectMapper) : AbpProjectNameViewComponent
    {
        private readonly ISessionAppService _sessionAppService = sessionAppService;
        private readonly IObjectMapper _objectMapper = objectMapper;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loginInfo = await _sessionAppService.GetCurrentLoginInformations();
            var model = _objectMapper.Map<TenantChangeViewModel>(loginInfo);
            return View(model);
        }
    }
}
