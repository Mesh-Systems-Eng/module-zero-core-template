﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Microsoft.IdentityModel.Tokens;
using System;

namespace AbpCompanyName.AbpProjectName.Authentication.JwtBearer;

public class TokenAuthConfiguration
{
    public SymmetricSecurityKey SecurityKey { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }

    public SigningCredentials SigningCredentials { get; set; }

    public TimeSpan Expiration { get; set; }
}
