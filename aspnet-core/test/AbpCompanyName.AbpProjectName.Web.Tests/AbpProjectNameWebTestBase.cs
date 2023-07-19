#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.AspNetCore.TestBase;
using Abp.Authorization.Users;
using Abp.Extensions;
using Abp.Json;
using Abp.MultiTenancy;
using Abp.Web.Models;
using AbpCompanyName.AbpProjectName.EntityFrameworkCore;
using AbpCompanyName.AbpProjectName.Models.TokenAuth;
using AbpCompanyName.AbpProjectName.Web.Startup;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CA2201 // Do not raise reserved exception types

namespace AbpCompanyName.AbpProjectName.Web.Tests
{
    public abstract class AbpProjectNameWebTestBase : AbpAspNetCoreIntegratedTestBase<Startup>
    {
        protected static readonly Lazy<string> ContentRootFolder;

        static AbpProjectNameWebTestBase() =>
            ContentRootFolder = new Lazy<string>(WebContentFolderHelper.CalculateContentRootFolder, true);

        // Html Parsing
        protected static IHtmlDocument ParseHtml(string htmlString) => new HtmlParser().ParseDocument(htmlString);

        protected override IWebHostBuilder CreateWebHostBuilder() =>
            base
            .CreateWebHostBuilder()
            .UseContentRoot(ContentRootFolder.Value)
            .UseSetting(WebHostDefaults.ApplicationKey, typeof(AbpProjectNameWebMvcModule).Assembly.FullName);

        // Get response

        protected async Task<T> GetResponseAsObjectAsync<T>(
            string url,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var strResponse = await GetResponseAsStringAsync(url, expectedStatusCode);
            return JsonConvert.DeserializeObject<T>(strResponse, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        protected async Task<string> GetResponseAsStringAsync(
            string url,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await GetResponseAsync(url, expectedStatusCode);
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<HttpResponseMessage> GetResponseAsync(
            string url,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await Client.GetAsync(url);
            response.StatusCode.Should().Be(expectedStatusCode);
            return response;
        }

        // Authenticate

        /// <summary>
        /// /api/TokenAuth/Authenticate
        /// TokenAuthController.
        /// </summary>
        /// <param name="tenancyName">Name of tenent.</param>
        /// <param name="input">An <see cref="AuthenticateModel"/> instance.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        protected async Task AuthenticateAsync(string tenancyName, AuthenticateModel input)
        {
            if (tenancyName.IsNullOrWhiteSpace())
            {
                var tenant = UsingDbContext(context => context.Tenants.FirstOrDefault(t => t.TenancyName == tenancyName));
                if (tenant != null)
                {
                    AbpSession.TenantId = tenant.Id;
                    Client.DefaultRequestHeaders.Add("Abp.TenantId", $"{tenant.Id}"); // Set TenantId
                }
            }

            var response = await Client.PostAsync(
                "/api/TokenAuth/Authenticate",
                new StringContent(input.ToJsonString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result =
                JsonConvert.DeserializeObject<AjaxResponse<AuthenticateResultModel>>(
                    await response.Content.ReadAsStringAsync());
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Result.AccessToken);

            AbpSession.UserId = result.Result.UserId;
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
                ?? throw new Exception("There is no user: " + userName + " for host.");
            AbpSession.UserId = user.Id;
        }

        protected void LoginAsTenant(string tenancyName, string userName)
        {
            var tenant = UsingDbContext(context => context.Tenants.FirstOrDefault(t => t.TenancyName == tenancyName))
                ?? throw new Exception("There is no tenant: " + tenancyName);
            AbpSession.TenantId = tenant.Id;

            var user =
                UsingDbContext(
                    context =>
                        context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == userName))
                ?? throw new Exception("There is no user: " + userName + " for tenant: " + tenancyName);
            AbpSession.UserId = user.Id;
        }

        // UsingDbContext

        protected void UsingDbContext(Action<AbpProjectNameDbContext> action)
        {
            using (var context = IocManager.Resolve<AbpProjectNameDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected T UsingDbContext<T>(Func<AbpProjectNameDbContext, T> func)
        {
            T result;

            using (var context = IocManager.Resolve<AbpProjectNameDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected async Task UsingDbContextAsync(Func<AbpProjectNameDbContext, Task> action)
        {
            using (var context = IocManager.Resolve<AbpProjectNameDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected async Task<T> UsingDbContextAsync<T>(Func<AbpProjectNameDbContext, Task<T>> func)
        {
            T result;

            using (var context = IocManager.Resolve<AbpProjectNameDbContext>())
            {
                result = await func(context);
                await context.SaveChangesAsync(true);
            }

            return result;
        }
    }
}
