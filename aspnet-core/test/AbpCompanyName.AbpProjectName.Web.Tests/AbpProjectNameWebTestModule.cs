#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using AbpCompanyName.AbpProjectName.EntityFrameworkCore;
using AbpCompanyName.AbpProjectName.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace AbpCompanyName.AbpProjectName.Web.Tests
{
    [DependsOn(
        typeof(AbpProjectNameWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule))]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0021:Use expression body for constructor", Justification = "Initial framework.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0022:Use expression body for method", Justification = "Initial framework.")]
    public class AbpProjectNameWebTestModule : AbpModule
    {
        public AbpProjectNameWebTestModule(AbpProjectNameEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <remarks>EF Core InMemory DB does not support transactions.</remarks>
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpProjectNameWebTestModule).GetAssembly());
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(AbpProjectNameWebMvcModule).Assembly);
        }
    }
}
