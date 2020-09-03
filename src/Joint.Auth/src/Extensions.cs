using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Joint.Auth.Handlers;
using Joint.Auth.Types;
using Joint.Auth.Factory;
using Joint.Auth.Services;
using Joint.Auth.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Joint.Auth
{
    public static class Extensions
    {
        private const string SectionName = "jwt";
        private const string RegistryName = "auth";

        public static IJointBuilder AddJwt(this IJointBuilder builder, string sectionName = SectionName,
            Action<JwtBearerOptions> optionsFactory = null)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var options = builder.GetOptions<JwtOptions>(sectionName);
            return builder.AddJwt(options, optionsFactory);
        }

        private static IJointBuilder AddJwt(this IJointBuilder builder, JwtOptions options,
            Action<JwtBearerOptions> optionsFactory = null)
        {
            if (!builder.TryRegister(RegistryName))
            {
                return builder;
            }

            builder.Services.AddTransient<IJwtHandler, JwtHandler>();
            builder.Services.AddSingleton<IAccessTokenService, InMemoryAccessTokenService>();
            builder.Services.AddTransient<AccessTokenValidatorMiddleware>();

            var tokenValidationParameters = TokenvalidationFactory.CreateParameters(options);
            tokenValidationParameters.AddIssuerSigningKey(options);
            tokenValidationParameters.AddAuthenticationType(options);
            tokenValidationParameters.AddNameClaimType(options);
            tokenValidationParameters.AddRoleClaimType(options);

            builder.Services
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
                {
                    option.Authority = options.Authority;
                    option.Audience = options.Audience;
                    option.MetadataAddress = options.MetadataAddress;
                    option.SaveToken = options.SaveToken;
                    option.RefreshOnIssuerKeyNotFound = options.RefreshOnIssuerKeyNotFound;
                    option.RequireHttpsMetadata = options.RequireHttpsMetadata;
                    option.IncludeErrorDetails = options.IncludeErrorDetails;
                    option.TokenValidationParameters = tokenValidationParameters;
                    if (!string.IsNullOrWhiteSpace(options.Challenge))
                    {
                        option.Challenge = options.Challenge;
                    }

                    optionsFactory?.Invoke(option);
                });

            builder.Services.AddSingleton(options);
            builder.Services.AddSingleton(tokenValidationParameters);

            return builder;
        }

        public static IApplicationBuilder UseAccessTokenValidator(this IApplicationBuilder app)
            => app.UseMiddleware<AccessTokenValidatorMiddleware>();
    }
}