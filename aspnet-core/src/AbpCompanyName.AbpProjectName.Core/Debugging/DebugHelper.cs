#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

namespace AbpCompanyName.AbpProjectName.Debugging
{
    public static class DebugHelper
    {
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
