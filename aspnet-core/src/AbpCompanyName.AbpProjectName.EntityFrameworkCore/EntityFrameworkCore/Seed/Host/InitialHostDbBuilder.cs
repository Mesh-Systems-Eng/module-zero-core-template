#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

namespace AbpCompanyName.AbpProjectName.EntityFrameworkCore.Seed.Host;

public class InitialHostDbBuilder(AbpProjectNameDbContext context)
{
    private readonly AbpProjectNameDbContext _context = context;

    public void Create()
    {
        new DefaultEditionCreator(_context).Create();
        new DefaultLanguagesCreator(_context).Create();
        new HostRoleAndUserCreator(_context).Create();
        new DefaultSettingsCreator(_context).Create();

        _context.SaveChanges();
    }
}
