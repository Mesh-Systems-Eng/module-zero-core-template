#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using AbpCompanyName.AbpProjectName.Authorization;
using AbpCompanyName.AbpProjectName.Controllers;
using AbpCompanyName.AbpProjectName.Roles;
using AbpCompanyName.AbpProjectName.Web.Models.Roles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Web.Controllers;

[AbpMvcAuthorize(PermissionNames.PagesRoles)]
public class RolesController(IRoleAppService roleAppService) : AbpProjectNameControllerBase
{
    private readonly IRoleAppService _roleAppService = roleAppService;

    public async Task<IActionResult> Index()
    {
        var permissions = (await _roleAppService.GetAllPermissions()).Items;
        var model = new RoleListViewModel
        {
            Permissions = permissions
        };

        return View(model);
    }

    public async Task<ActionResult> EditModal(int roleId)
    {
        var output = await _roleAppService.GetRoleForEdit(new EntityDto(roleId));
        var model = ObjectMapper.Map<EditRoleModalViewModel>(output);

        return PartialView("_EditModal", model);
    }
}
