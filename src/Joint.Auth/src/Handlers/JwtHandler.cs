using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Joint.Auth.Types;
using Joint.Auth.Dates;
using Joint.Auth.Options;

namespace Joint.Auth.Handlers
{
    internal sealed class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly SigningCredentials _signingCredentials;

        public JwtHandler(JwtOptions options, TokenValidationParameters tokenValidationParameters)
        {
            var issuerSigningKey = tokenValidationParameters.IssuerSigningKey;
            if (issuerSigningKey is null)
            {
                throw new InvalidOperationException("Issuer signing key not set.");
            }

            if (string.IsNullOrWhiteSpace(options.Algorithm))
            {
                throw new InvalidOperationException("Security algorithm not set.");
            }

            _options = options;
            _tokenValidationParameters = tokenValidationParameters;
            _signingCredentials = new SigningCredentials(issuerSigningKey, _options.Algorithm);
        }

        public JsonWebToken CreateToken(string userId, string email)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID claim (subject) cannot be empty.", userId);
            }

            var now = DateTime.UtcNow;
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
            };

            if (!string.IsNullOrWhiteSpace(email))
            {
                jwtClaims.Add(new Claim(ServerClaimNames.Email, email));
            }

            var expires = _options.Expiry.HasValue
                ? now.AddMilliseconds(_options.Expiry.Value.TotalMilliseconds)
                : now.AddMinutes(_options.ExpiryMinutes);

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.ValidAudience,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                RefreshToken = string.Empty,
                Id = userId,
            };
        }

        public JsonWebRefreshToken CreateRefreshToken(string accessToken, Guid userId)
        {
            _jwtSecurityTokenHandler.ValidateToken(accessToken, _tokenValidationParameters,
                out var validatedSecurityToken);
            if (!(validatedSecurityToken is JwtSecurityToken jwt))
            {
                return null;
            }
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[128];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new JsonWebRefreshToken(new Guid(), userId, Convert.ToBase64String(randomBytes), jwt.ValidTo, DateTime.Now);
        }
    }
}