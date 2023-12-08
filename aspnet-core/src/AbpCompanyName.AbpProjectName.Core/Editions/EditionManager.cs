#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;

namespace AbpCompanyName.AbpProjectName.Editions;

public class EditionManager(
    IRepository<Edition> editionRepository,
    IAbpZeroFeatureValueStore featureValueStore,
    IUnitOfWorkManager unitOfWorkManager) : AbpEditionManager(editionRepository, featureValueStore, unitOfWorkManager)
{
    public const string DefaultEditionName = "Standard";
}
