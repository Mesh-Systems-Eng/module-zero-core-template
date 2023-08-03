#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate
// Contributions Copyright © 2023 Mesh Systems LLC

namespace AbpCompanyName.AbpProjectName.Debugging
{
    public static class DebugHelper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0079:Remove unnecessary suppression", Justification = "Initial framework.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0025:Use expression body for properties", Justification = "Initial framework.")]
        public static bool IsDebug
        {
            get
            {
#pragma warning disable
#if DEBUG
                return true;
#endif
                return false;
#pragma warning restore
            }
        }
    }
}
