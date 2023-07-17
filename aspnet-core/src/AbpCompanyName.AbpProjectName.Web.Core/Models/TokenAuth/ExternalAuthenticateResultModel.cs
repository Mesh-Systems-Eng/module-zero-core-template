#pragma warning disable IDE0073
// Copyright © 2016 ASP.NET Boilerplate

namespace AbpCompanyName.AbpProjectName.Models.TokenAuth
{
    public class ExternalAuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public bool WaitingForActivation { get; set; }
    }
}
