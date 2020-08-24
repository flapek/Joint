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
                RequireSignedTokens = options.RequireSignedTokens,
                ValidateIssuer = options.ValidateIssuer,
                ValidIssuer = options.Issuer,
                ValidateAudience = options.ValidateAudience,
                ValidAudience = options.Audience,
                ValidateLifetime = options.ValidateLifetime,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = options.RequireExpirationTime,
            };
        public static void AddIssuerSigningKey(this TokenValidationParameters tokenValidationParameters,
            JwtOptions options)
        {
            tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.IssuerSigningKey));
            Console.WriteLine("Using symmetric encryption for issuing tokens.");
        }
    }
}
