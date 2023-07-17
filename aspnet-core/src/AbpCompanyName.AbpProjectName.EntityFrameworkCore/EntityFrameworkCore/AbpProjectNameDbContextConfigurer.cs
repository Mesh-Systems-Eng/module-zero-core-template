#pragma warning disable IDE0073
// Copyright � 2016 ASP.NET Boilerplate
// Contributions Copyright � 2023 Mesh Systems LLC

using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace AbpCompanyName.AbpProjectName.EntityFrameworkCore
{
    public static class AbpProjectNameDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AbpProjectNameDbContext> builder, string connectionString) =>
            builder.UseSqlServer(connectionString);

        public static void Configure(DbContextOptionsBuilder<AbpProjectNameDbContext> builder, DbConnection connection) =>
            builder.UseSqlServer(connection);
    }
}
