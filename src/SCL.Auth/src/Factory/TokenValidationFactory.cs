using Microsoft.IdentityModel.Tokens;
using SCL.Auth.Types;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SCL.Auth.Factory
{
    internal static class TokenvalidationFactory
    {
        public static TokenValidationParameters CreateParameters(JwtOptions options)
            => new TokenValidationParameters
            {
                RequireAudience = options.RequireAudience,
                ValidIssuer = options.ValidIssuer,
                ValidIssuers = options.ValidIssuers,
                ValidateActor = options.ValidateActor,
                ValidAudience = options.ValidAudience,
                ValidAudiences = options.ValidAudiences,
                ValidateAudience = options.ValidateAudience,
                ValidateIssuer = options.ValidateIssuer,
                ValidateLifetime = options.ValidateLifetime,
                ValidateTokenReplay = options.ValidateTokenReplay,
                ValidateIssuerSigningKey = options.ValidateIssuerSigningKey,
                SaveSigninToken = options.SaveSigninToken,
                RequireExpirationTime = options.RequireExpirationTime,
                RequireSignedTokens = options.RequireSignedTokens,
                ClockSkew = TimeSpan.Zero
            };

        public static void AddAuthenticationType(this TokenValidationParameters tokenValidationParameters,
            JwtOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.AuthenticationType))
            {
                tokenValidationParameters.AuthenticationType = options.AuthenticationType;
            }
        }

        public static void AddIssuerSigningKey(this TokenValidationParameters tokenValidationParameters,
            JwtOptions options)
        {
            var hasCertificate = false;
            if (options.Certificate is { })
            {
                X509Certificate2 certificate = null;
                var password = options.Certificate.Password;
                var hasPassword = !string.IsNullOrWhiteSpace(password);
                if (!string.IsNullOrWhiteSpace(options.Certificate.Location))
                {
                    certificate = hasPassword
                        ? new X509Certificate2(options.Certificate.Location, password)
                        : new X509Certificate2(options.Certificate.Location);
                    var keyType = certificate.HasPrivateKey ? "with private key" : "with public key only";
                    Console.WriteLine($"Loaded X.509 certificate from location: '{options.Certificate.Location}' {keyType}.");
                }

                if (!string.IsNullOrWhiteSpace(options.Certificate.RawData))
                {
                    var rawData = Convert.FromBase64String(options.Certificate.RawData);
                    certificate = hasPassword
                        ? new X509Certificate2(rawData, password)
                        : new X509Certificate2(rawData);
                    var keyType = certificate.HasPrivateKey ? "with private key" : "with public key only";
                    Console.WriteLine($"Loaded X.509 certificate from raw data {keyType}.");
                }

                if (certificate is { })
                {
                    if (string.IsNullOrWhiteSpace(options.Algorithm))
                    {
                        options.Algorithm = SecurityAlgorithms.RsaSha256;
                    }

                    hasCertificate = true;
                    tokenValidationParameters.IssuerSigningKey = new X509SecurityKey(certificate);
                    var actionType = certificate.HasPrivateKey ? "issuing" : "validating";
                    Console.WriteLine($"Using X.509 certificate for {actionType} tokens.");
                }
            }
            if (!string.IsNullOrWhiteSpace(options.IssuerSigningKey) && !hasCertificate)
            {
                if (string.IsNullOrWhiteSpace(options.Algorithm) || hasCertificate)
                {
                    options.Algorithm = SecurityAlgorithms.HmacSha256;
                }

                var rawKey = Encoding.UTF8.GetBytes(options.IssuerSigningKey);
                tokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(rawKey);
                Console.WriteLine("Using symmetric encryption for issuing tokens.");
            }
        }

        public static void AddNameClaimType(this TokenValidationParameters tokenValidationParameters,
            JwtOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.NameClaimType))
            {
                tokenValidationParameters.NameClaimType = options.NameClaimType;
            }
        }

        public static void AddRoleClaimType(this TokenValidationParameters tokenValidationParameters,
            JwtOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.RoleClaimType))
            {
                tokenValidationParameters.RoleClaimType = options.RoleClaimType;
            }
        }
    }
}
