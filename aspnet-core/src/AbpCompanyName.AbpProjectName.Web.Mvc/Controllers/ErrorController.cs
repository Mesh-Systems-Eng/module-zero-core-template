﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.AspNetCore.Mvc.Controllers;
using Abp.Web.Models;
using Abp.Web.Mvc.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;

#pragma warning disable CA2201 // Do not raise reserved exception types

namespace AbpCompanyName.AbpProjectName.Web.Controllers;

public class ErrorController(IErrorInfoBuilder errorInfoBuilder) : AbpController
{
    private readonly IErrorInfoBuilder _errorInfoBuilder = errorInfoBuilder;

    public ActionResult Index()
    {
        var exHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();

        var exception = exHandlerFeature != null
                            ? exHandlerFeature.Error
                            : new Exception("Unhandled exception!");

        return View(
            "Error",
            new ErrorViewModel(
                _errorInfoBuilder.BuildForException(exception),
                exception));
    }
}
