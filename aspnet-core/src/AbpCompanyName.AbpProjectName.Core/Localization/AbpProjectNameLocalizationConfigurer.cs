#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace AbpCompanyName.AbpProjectName.Localization
{
    public static class AbpProjectNameLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration) =>
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    AbpProjectNameConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AbpProjectNameLocalizationConfigurer).GetAssembly(),
                        "AbpCompanyName.AbpProjectName.Localization.SourceFiles")));
    }
}
