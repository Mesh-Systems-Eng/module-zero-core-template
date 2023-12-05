#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.Models.TokenAuth;
using AbpCompanyName.AbpProjectName.Web.Controllers;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace AbpCompanyName.AbpProjectName.Web.Tests.Controllers;

public class HomeControllerTests : AbpProjectNameWebTestBase
{
    [Fact]
    public async Task Index_Test()
    {
        await AuthenticateAsync(null, new AuthenticateModel
        {
            UserNameOrEmailAddress = "admin",
            Password = User.DefaultPassword
        });

        // Act
        var response = await GetResponseAsStringAsync(
            GetUrl<HomeController>(nameof(HomeController.Index)));

        // Assert
        response.Should().NotBeNullOrEmpty();
    }
}
