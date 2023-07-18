#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

using Abp.AutoMapper;
using AbpCompanyName.AbpProjectName.Authentication.External;

namespace AbpCompanyName.AbpProjectName.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
