using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Convey;
using Epilepsy_Health_App.Service.Common.Auth.Core;
using Epilepsy_Health_App.Service.Common.Auth.Infrastructure.Factory;

namespace Epilepsy_Health_App.Service.Common.Auth.Infrastructure
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
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.Authority = options.Authority;
                    o.Audience = options.Audience;
                    o.MetadataAddress = options.MetadataAddress;
                    o.SaveToken = options.SaveToken;
                    o.RefreshOnIssuerKeyNotFound = options.RefreshOnIssuerKeyNotFound;
                    o.RequireHttpsMetadata = options.RequireHttpsMetadata;
                    o.IncludeErrorDetails = options.IncludeErrorDetails;
                    o.TokenValidationParameters = tokenValidationParameters;
                    if (!string.IsNullOrWhiteSpace(options.Challenge))
                    {
                        o.Challenge = options.Challenge;
                    }

                    optionsFactory?.Invoke(o);
                });

            builder.Services.AddSingleton(options);
            builder.Services.AddSingleton(tokenValidationParameters);

            return builder;
        }

        public static IApplicationBuilder UseJwt(this IApplicationBuilder app)
            => app
            .UseAuthentication()
            .UseAuthorization();
    }
}