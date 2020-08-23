using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SCL.Auth.Core.Dates;
using Microsoft.IdentityModel.Tokens;
using SCL.Auth.Core.Types;
using SCL.Auth.Core;
using System.Security.Cryptography;

namespace SCL.Auth.Application.Handlers
{
    public sealed class JwtHandler : IJwtHandler
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

        public JsonWebToken CreateToken(User user)
        {
            if (string.IsNullOrWhiteSpace(user.UserID))
            {
                throw new ArgumentException("User ID claim (subject) cannot be empty.", nameof(user.UserID));
            }

            var now = DateTime.UtcNow;
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserID),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
            };

            if (!string.IsNullOrWhiteSpace(user.Username))
            {
                jwtClaims.Add(new Claim(ServerClaimNames.Username, user.Username));
            }

            if (!string.IsNullOrWhiteSpace(user.ServerToken))
            {
                jwtClaims.Add(new Claim(ServerClaimNames.ServerToken, user.ServerToken));
            }

            if (!string.IsNullOrWhiteSpace(user.ServerName))
            {
                jwtClaims.Add(new Claim(ServerClaimNames.ServerName, user.ServerName));
            }

            var expires = _options.Expiry.HasValue
                ? now.AddMilliseconds(_options.Expiry.Value.TotalMilliseconds)
                : now.AddMinutes(_options.ExpiryMinutes);

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
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
                Expires = expires.ToTimestamp(),
                Id = user.UserID,
            };
        }

        public JsonWebRefreshToken CreateRefreshToken(string accessToken)
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
            return new JsonWebRefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = jwt.ValidTo.ToTimestamp(),
                Created = DateTime.Now
            };
        }
    }
}