#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

using Abp.Dependency;
using Abp.Timing;
using Castle.Core.Logging;
using System;

namespace AbpCompanyName.AbpProjectName.Migrator
{
    public class Log : ITransientDependency
    {
        public Log()
        {
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void Write(string text)
        {
            Console.WriteLine($"{Clock.Now:yyyy-MM-dd HH:mm:ss} | {text}");
            Logger.Info(text);
        }
    }
}
