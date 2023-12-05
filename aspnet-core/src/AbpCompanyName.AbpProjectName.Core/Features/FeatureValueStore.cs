#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.MultiTenancy;

namespace AbpCompanyName.AbpProjectName.Features;

public class FeatureValueStore(
    ICacheManager cacheManager,
    IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
    IRepository<Tenant> tenantRepository,
    IRepository<EditionFeatureSetting, long> editionFeatureRepository,
    IFeatureManager featureManager,
    IUnitOfWorkManager unitOfWorkManager) : AbpFeatureValueStore<Tenant, User>(
          cacheManager,
          tenantFeatureRepository,
          tenantRepository,
          editionFeatureRepository,
          featureManager,
          unitOfWorkManager)
{
}
