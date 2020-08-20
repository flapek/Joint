using System;

namespace Epilepsy_Health_App.Service.Common.Auth.Core
{
    public class JsonWebRefreshToken
    {
        public string Token { get; set; }
        public long Expires { get; set; }
        public DateTime Created { get; set; }
    }
}