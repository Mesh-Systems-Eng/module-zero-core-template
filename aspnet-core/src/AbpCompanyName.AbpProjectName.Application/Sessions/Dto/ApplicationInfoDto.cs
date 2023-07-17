#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

using System;
using System.Collections.Generic;

namespace AbpCompanyName.AbpProjectName.Sessions.Dto
{
    public class ApplicationInfoDto
    {
        public string Version { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Dictionary<string, bool> Features { get; set; }
    }
}
