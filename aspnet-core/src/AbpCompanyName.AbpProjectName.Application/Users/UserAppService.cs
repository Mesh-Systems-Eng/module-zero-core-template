﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

#pragma warning disable CA2201 // Do not raise reserved exception types
#pragma warning disable CA1725 // Parameter names should match base declaration

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using AbpCompanyName.AbpProjectName.Authorization;
using AbpCompanyName.AbpProjectName.Authorization.Roles;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.Roles.Dto;
using AbpCompanyName.AbpProjectName.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Users;

[AbpAuthorize(PermissionNames.PagesUsers)]
public class UserAppService(
    IRepository<User, long> repository,
    UserManager userManager,
    RoleManager roleManager,
    IRepository<Role> roleRepository,
    IPasswordHasher<User> passwordHasher,
    IAbpSession abpSession,
    LogInManager logInManager) : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>(repository), IUserAppService
{
    private readonly UserManager _userManager = userManager;
    private readonly RoleManager _roleManager = roleManager;
    private readonly IRepository<Role> _roleRepository = roleRepository;
    private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
    private readonly IAbpSession _abpSession = abpSession;
    private readonly LogInManager _logInManager = logInManager;

    public override async Task<UserDto> CreateAsync(CreateUserDto input)
    {
        CheckCreatePermission();

        var user = ObjectMapper.Map<User>(input);

        user.TenantId = AbpSession.TenantId;
        user.IsEmailConfirmed = true;

        await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

        CheckErrors(await _userManager.CreateAsync(user, input.Password));

        if (input.RoleNames != null)
        {
            CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
        }

        CurrentUnitOfWork.SaveChanges();

        return MapToEntityDto(user);
    }

    public override async Task<UserDto> UpdateAsync(UserDto input)
    {
        CheckUpdatePermission();

        var user = await _userManager.GetUserByIdAsync(input.Id);

        MapToEntity(input, user);

        CheckErrors(await _userManager.UpdateAsync(user));

        if (input.RoleNames != null)
        {
            CheckErrors(await _userManager.SetRolesAsync(user, input.RoleNames));
        }

        return await GetAsync(input);
    }

    public override async Task DeleteAsync(EntityDto<long> input)
    {
        var user = await _userManager.GetUserByIdAsync(input.Id);
        await _userManager.DeleteAsync(user);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    [AbpAuthorize(PermissionNames.PagesUsersActivation)]
    [SuppressMessage("Style", "IDE0022:Use expression body for method", Justification = "Initial framework.")]
    public async Task Activate(EntityDto<long> user)
    {
        await Repository.UpdateAsync(user.Id, async (entity) =>
        {
            entity.IsActive = true;
        });
    }

    [AbpAuthorize(PermissionNames.PagesUsersActivation)]
    [SuppressMessage("Style", "IDE0022:Use expression body for method", Justification = "Initial framework.")]
    public async Task DeActivate(EntityDto<long> user)
    {
        await Repository.UpdateAsync(user.Id, async (entity) =>
        {
            entity.IsActive = false;
        });
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    public async Task<ListResultDto<RoleDto>> GetRoles()
    {
        var roles = await _roleRepository.GetAllListAsync();
        return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
    }

    public async Task ChangeLanguage(ChangeUserLanguageDto input) =>
        await SettingManager.ChangeSettingForUserAsync(
            AbpSession.ToUserIdentifier(),
            LocalizationSettingNames.DefaultLanguage,
            input.LanguageName);

    public async Task<bool> ChangePassword(ChangePasswordDto input)
    {
        await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

        var user = await _userManager.FindByIdAsync($"{AbpSession.GetUserId()}")
            ?? throw new Exception("There is no current user!");

        if (await _userManager.CheckPasswordAsync(user, input.CurrentPassword))
        {
            CheckErrors(await _userManager.ChangePasswordAsync(user, input.NewPassword));
        }
        else
        {
            CheckErrors(IdentityResult.Failed(new IdentityError
            {
                Description = "Incorrect password."
            }));
        }

        return true;
    }

    public async Task<bool> ResetPassword(ResetPasswordDto input)
    {
        if (_abpSession.UserId == null)
        {
            throw new UserFriendlyException("Please log in before attempting to reset password.");
        }

        var currentUser = await _userManager.GetUserByIdAsync(_abpSession.GetUserId());
        var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
        if (loginAsync.Result != AbpLoginResultType.Success)
        {
            throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
        }

        if (currentUser.IsDeleted || !currentUser.IsActive)
        {
            return false;
        }

        var roles = await _userManager.GetRolesAsync(currentUser);
        if (!roles.Contains(StaticRoleNames.Tenants.Admin))
        {
            throw new UserFriendlyException("Only administrators may reset passwords.");
        }

        var user = await _userManager.GetUserByIdAsync(input.UserId);
        if (user != null)
        {
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        return true;
    }

    protected override User MapToEntity(CreateUserDto createInput)
    {
        var user = ObjectMapper.Map<User>(createInput);
        user.SetNormalizedNames();

        return user;
    }

    protected override void MapToEntity(UserDto input, User user)
    {
        ObjectMapper.Map(input, user);
        user.SetNormalizedNames();
    }

    protected override UserDto MapToEntityDto(User user)
    {
        var roleIds = user.Roles.Select(x => x.RoleId).ToArray();

        var roles = _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).Select(r => r.NormalizedName);

        var userDto = base.MapToEntityDto(user);
        userDto.RoleNames = [.. roles];

        return userDto;
    }

    protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input) =>
        Repository.GetAllIncluding(x => x.Roles)
        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
        .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);

    protected override async Task<User> GetEntityByIdAsync(long id)
    {
        var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new EntityNotFoundException(typeof(User), id);

        return user;
    }

    protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input) =>
        query.OrderBy(r => r.UserName);

    protected virtual void CheckErrors(IdentityResult identityResult) =>
        identityResult.CheckErrors(LocalizationManager);
}
