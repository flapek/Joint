using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Convey;
using SCL.Auth.Core;
using SCL.Auth.Infrastructure.Factory;
using SCL.Auth.Application;
using SCL.Auth.Application.Handlers;

namespace SCL.Auth.Infrastructure
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
            tokenValidationParameters.AddAuthenticationType(options);
            var hasCertificate = tokenValidationParameters.AddCertificate(options);
            tokenValidationParameters.AddIssuerSigningKey(options, hasCertificate);
            tokenValidationParameters.AddNameClaimType(options);
            tokenValidationParameters.AddRoleClaimType(options);

            builder.Services
                .AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options.AuthenticationProviderKey , option =>
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
            builder.Services.AddTransient<IJwtHandler, JwtHandler>();

            return builder;
        }

        public static IApplicationBuilder UseJwt(this IApplicationBuilder app)
            => app
            .UseAuthentication()
            .UseAuthorization();
    }
}