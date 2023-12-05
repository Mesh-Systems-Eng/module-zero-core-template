#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using AbpCompanyName.AbpProjectName.Sessions;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace AbpCompanyName.AbpProjectName.Tests.Sessions;

public class SessionAppServiceTests : AbpProjectNameTestBase
{
    private readonly ISessionAppService _sessionAppService;

    public SessionAppServiceTests() =>
        _sessionAppService = Resolve<ISessionAppService>();

    [MultiTenantFact]
    public async Task Should_Get_Current_User_When_Logged_In_As_Host()
    {
        // Arrange
        LoginAsHostAdmin();

        // Act
        var output = await _sessionAppService.GetCurrentLoginInformations();

        // Assert
        var currentUser = await GetCurrentUserAsync();
        output.User.Should().NotBeNull();
        output.User.Name.Should().Be(currentUser.Name);
        output.User.Surname.Should().Be(currentUser.Surname);

        output.Tenant.Should().BeNull();
    }

    [Fact]
    public async Task Should_Get_Current_User_And_Tenant_When_Logged_In_As_Tenant()
    {
        // Act
        var output = await _sessionAppService.GetCurrentLoginInformations();

        // Assert
        var currentUser = await GetCurrentUserAsync();
        var currentTenant = await GetCurrentTenantAsync();

        output.User.Should().NotBeNull();
        output.User.Name.Should().Be(currentUser.Name);

        output.Tenant.Should().NotBeNull();
        output.Tenant.Name.Should().Be(currentTenant.Name);
    }
}
