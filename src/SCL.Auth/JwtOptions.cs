using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;

namespace SCL.Auth.Core
{
    public class JwtOptions
    {
        public string AuthenticationProviderKey { get; set; }
        public string Algorithm { get; set; }
        public string Issuer { get; set; }
        public string IssuerSigningKey { get; set; }
        public string Audience { get; set; }
        public bool RequireHttpsMetadata { get; set; } = true;
        public bool RequireExpirationTime { get; set; } = true;
        public int ExpiryMinutes { get; set; }
        public TimeSpan? Expiry { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public bool ValidateAudience { get; set; } = true;
        public bool ValidateIssuer { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
        public bool ValidateIssuerSigningKey { get; set; }
        public string AuthenticateSchema { get; set; }

    }
}