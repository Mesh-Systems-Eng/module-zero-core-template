﻿#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

namespace AbpCompanyName.AbpProjectName.Web.Models.Common.Modals;

public class ModalHeaderViewModel(string title)
{
    public string Title { get; set; } = title;
}
