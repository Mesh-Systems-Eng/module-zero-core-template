#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Authorization;
using Abp.Runtime.Session;
using AbpCompanyName.AbpProjectName.Configuration.Dto;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : AbpProjectNameAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input) => await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
