#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Application.Navigation;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Web.Views.Shared.Components.SideBarMenu
{
    public class SideBarMenuViewComponent(
        IUserNavigationManager userNavigationManager,
        IAbpSession abpSession) : AbpProjectNameViewComponent
    {
        private readonly IUserNavigationManager _userNavigationManager = userNavigationManager;
        private readonly IAbpSession _abpSession = abpSession;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new SideBarMenuViewModel
            {
                MainMenu = await _userNavigationManager.GetMenuAsync("MainMenu", _abpSession.ToUserIdentifier())
            };

            return View(model);
        }
    }
}
