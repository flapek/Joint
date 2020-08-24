using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Convey;
using SCL.Auth.Handlers;
using SCL.Auth.Types;
using SCL.Auth.Factory;

namespace SCL.Auth
{
    public static class Extensions
    {
        private const string SectionName = "jwt";
        private const string RegistryName = "auth";

        public static IConveyBuilder AddJwt(this IConveyBuilder builder, string sectionName = SectionName,
            Action<JwtBearerOptions> optionsFactory = null)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            builder.Services.AddTransient<IJwtHandler, JwtHandler>();

            var options = builder.GetOptions<JwtOptions>(sectionName);
            return builder.AddJwt(options, optionsFactory);
        }

        private static IConveyBuilder AddJwt(this IConveyBuilder builder, JwtOptions options,
            Action<JwtBearerOptions> optionsFactory = null)
        {
            if (!builder.TryRegister(RegistryName))
            {
                return builder;
            }

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

    }
}