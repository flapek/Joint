using Microsoft.IdentityModel.Tokens;
using SCL.Auth.Core;
using System;
using System.Text;

namespace SCL.Auth.Infrastructure.Factory
{
    internal static class TokenvalidationFactory
    {
        public static TokenValidationParameters CreateParameters(JwtOptions options)
            => new TokenValidationParameters
            {
                ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
                ValidateIssuer = options.ValidateIssuer,
                ValidIssuer = options.ValidIssuer,
                ValidateAudience = options.ValidateAudience,
                ValidAudience = options.ValidAudience,
                ValidateLifetime = options.ValidateLifetime,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = options.RequireExpirationTime,
            };
        public static void AddIssuerSigningKey(this TokenValidationParameters tokenValidationParameters,
            JwtOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.Algorithm))
            {
                options.Algorithm = SecurityAlgorithms.HmacSha256;
            }

            tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.IssuerSigningKey));
            Console.WriteLine("Using symmetric encryption for issuing tokens.");
        }
    }
}
