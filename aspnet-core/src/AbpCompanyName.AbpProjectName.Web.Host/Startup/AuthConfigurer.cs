﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Runtime.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpCompanyName.AbpProjectName.Web.Host.Startup;

public static class AuthConfigurer
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.Audience = configuration["Authentication:JwtBearer:Audience"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = QueryStringTokenResolver
                };
            });
        }
    }

    /// <summary>
    /// This method is needed to authorize SignalR javascript client.
    /// SignalR can not send authorization header. So, we are getting it from query string as an encrypted text.
    /// </summary>
    /// <param name="context">The <see cref="MessageReceivedContext"/> for the current message.</param>
    /// <returns>A <see cref="Task"/>.</returns>
    private static Task QueryStringTokenResolver(MessageReceivedContext context)
    {
        if (!context.HttpContext.Request.Path.HasValue ||
            !context.HttpContext.Request.Path.Value.StartsWith("/signalr", StringComparison.OrdinalIgnoreCase))
        {
            // We are just looking for signalr clients
            return Task.CompletedTask;
        }

        var qsAuthToken = context.HttpContext.Request.Query["enc_auth_token"].FirstOrDefault();
        if (qsAuthToken == null)
        {
            // Cookie value does not matches to querystring value
            return Task.CompletedTask;
        }

        // Set auth token from cookie
        context.Token = SimpleStringCipher.Instance.Decrypt(qsAuthToken);
        return Task.CompletedTask;
    }
}
