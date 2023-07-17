#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp;
using Abp.Authorization.Users;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.TestBase;
using AbpCompanyName.AbpProjectName.Authorization.Users;
using AbpCompanyName.AbpProjectName.EntityFrameworkCore;
using AbpCompanyName.AbpProjectName.EntityFrameworkCore.Seed.Host;
using AbpCompanyName.AbpProjectName.EntityFrameworkCore.Seed.Tenants;
using AbpCompanyName.AbpProjectName.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

#pragma warning disable CA2201 // Do not raise reserved exception types

namespace AbpCompanyName.AbpProjectName.Tests
{
    public abstract class AbpProjectNameTestBase : AbpIntegratedTestBase<AbpProjectNameTestModule>
    {
        protected AbpProjectNameTestBase()
        {
            static void NormalizeDbContext(AbpProjectNameDbContext context)
            {
                context.EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
                context.EventBus = NullEventBus.Instance;
                context.SuppressAutoSetTenantId = true;
            }

            // Seed initial data for host
            AbpSession.TenantId = null;
            UsingDbContext(context =>
            {
                NormalizeDbContext(context);
                new InitialHostDbBuilder(context).Create();
                new DefaultTenantBuilder(context).Create();
            });

            // Seed initial data for default tenant
            AbpSession.TenantId = 1;
            UsingDbContext(context =>
            {
                NormalizeDbContext(context);
                new TenantRoleAndUserBuilder(context, 1).Create();
            });

            LoginAsDefaultTenantAdmin();
        }

        // UsingDbContext

        protected IDisposable UsingTenantId(int? tenantId)
        {
            var previousTenantId = AbpSession.TenantId;
            AbpSession.TenantId = tenantId;
            return new DisposeAction(() => AbpSession.TenantId = previousTenantId);
        }

        protected void UsingDbContext(Action<AbpProjectNameDbContext> action) =>
            UsingDbContext(AbpSession.TenantId, action);

        protected Task UsingDbContextAsync(Func<AbpProjectNameDbContext, Task> action) =>
            UsingDbContextAsync(AbpSession.TenantId, action);

        protected T UsingDbContext<T>(Func<AbpProjectNameDbContext, T> func) =>
            UsingDbContext(AbpSession.TenantId, func);

        protected Task<T> UsingDbContextAsync<T>(Func<AbpProjectNameDbContext, Task<T>> func) =>
            UsingDbContextAsync(AbpSession.TenantId, func);

        protected void UsingDbContext(int? tenantId, Action<AbpProjectNameDbContext> action)
        {
            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<AbpProjectNameDbContext>())
                {
                    action(context);
                    context.SaveChanges();
                }
            }
        }

        protected async Task UsingDbContextAsync(int? tenantId, Func<AbpProjectNameDbContext, Task> action)
        {
            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<AbpProjectNameDbContext>())
                {
                    await action(context);
                    await context.SaveChangesAsync();
                }
            }
        }

        protected T UsingDbContext<T>(int? tenantId, Func<AbpProjectNameDbContext, T> func)
        {
            T result;

            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<AbpProjectNameDbContext>())
                {
                    result = func(context);
                    context.SaveChanges();
                }
            }

            return result;
        }

        protected async Task<T> UsingDbContextAsync<T>(int? tenantId, Func<AbpProjectNameDbContext, Task<T>> func)
        {
            T result;

            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<AbpProjectNameDbContext>())
                {
                    result = await func(context);
                    await context.SaveChangesAsync();
                }
            }

            return result;
        }

        // Login

        protected void LoginAsHostAdmin() =>
            LoginAsHost(AbpUserBase.AdminUserName);

        protected void LoginAsDefaultTenantAdmin() =>
            LoginAsTenant(AbpTenantBase.DefaultTenantName, AbpUserBase.AdminUserName);

        protected void LoginAsHost(string userName)
        {
            AbpSession.TenantId = null;

            var user =
                UsingDbContext(
                    context =>
                    context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == userName))
                ?? throw new Exception($"There is no user: {userName} for host.");

            AbpSession.UserId = user.Id;
        }

        protected void LoginAsTenant(string tenancyName, string userName)
        {
            var tenant = UsingDbContext(context => context.Tenants.FirstOrDefault(t => t.TenancyName == tenancyName))
                ?? throw new Exception($"There is no tenant: {tenancyName}.");

            AbpSession.TenantId = tenant.Id;

            var user =
                UsingDbContext(
                    context =>
                        context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == userName))
                ?? throw new Exception($"There is no user: {userName} for tenant: {tenancyName}.");

            AbpSession.UserId = user.Id;
        }

        /// <summary>
        /// Gets current user if <see cref="IAbpSession.UserId"/> is not null.
        /// Throws exception if it's null.
        /// </summary>
        /// <returns>A <see cref="Task"/> of type <see cref="User"/> representing the asynchronous operation.</returns>
        protected async Task<User> GetCurrentUserAsync()
        {
            var userId = AbpSession.GetUserId();
            return await UsingDbContext(context => context.Users.SingleAsync(u => u.Id == userId));
        }

        /// <summary>
        /// Gets current tenant if <see cref="IAbpSession.TenantId"/> is not null.
        /// Throws exception if there is no current tenant.
        /// </summary>
        /// <returns>A <see cref="Task"/> of type <see cref="Tenant"/> representing the asynchronous operation.</returns>
        protected async Task<Tenant> GetCurrentTenantAsync()
        {
            var tenantId = AbpSession.GetTenantId();
            return await UsingDbContext(context => context.Tenants.SingleAsync(t => t.Id == tenantId));
        }
    }
}
