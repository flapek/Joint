using System;

namespace SCL.Auth.Core
{
    public class JwtOptions
    {
        public string Issuer { get; set; }
        public string IssuerSigningKey { get; set; }
        public string Audience { get; set; }
        public int ExpiryMinutes { get; set; }
        public TimeSpan? Expiry { get; set; }
        public bool RequireHttpsMetadata { get; set; } = false;
        public bool RequireExpirationTime { get; set; } = true;
        public bool ValidateAudience { get; set; } = true;
        public bool ValidateIssuer { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
        public bool ValidateIssuerSigningKey { get; set; }

    }
}