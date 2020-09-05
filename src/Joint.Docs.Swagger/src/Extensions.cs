using System;
using Joint.Docs.Swagger.Builders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Joint.Docs.Swagger
{
    public static class Extensions
    {
        private const string SectionName = "swagger";
        private const string RegistryName = "docs.swagger";

        public static IJointBuilder AddSwaggerDocs(this IJointBuilder builder, string xmlPath, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var options = builder.GetOptions<SwaggerOptions>(sectionName);
            return builder.AddSwaggerDocs(options, xmlPath);
        }

        public static IJointBuilder AddSwaggerDocs(this IJointBuilder builder,
            Func<ISwaggerOptionsBuilder, ISwaggerOptionsBuilder> buildOptions, string xmlPath)
        {
            var options = buildOptions(new SwaggerOptionsBuilder()).Build();
            return builder.AddSwaggerDocs(options, xmlPath);
        }

        public static IJointBuilder AddSwaggerDocs(this IJointBuilder builder, SwaggerOptions options, string xmlPath)
        {
            if (!options.Enabled || !builder.TryRegister(RegistryName))
            {
                return builder;
            }

            builder.Services.AddSingleton(options);
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Name,
                    new OpenApiInfo
                    {
                        Title = options.Title,
                        Version = options.Version,
                        Contact = new OpenApiContact
                        {
                            Name = options.ContactName,
                            Email = options.ContactEmail
                        }
                    });

                if (!string.IsNullOrWhiteSpace(xmlPath))
                    c.IncludeXmlComments(xmlPath);

                if (options.IncludeSecurity)
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description =
                            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                }
            });

            return builder;
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetService<SwaggerOptions>();
            if (!options.Enabled)
            {
                return builder;
            }

            var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "swagger" : options.RoutePrefix;

            builder.UseStaticFiles()
                .UseSwagger(c => c.RouteTemplate = routePrefix + "/{documentName}/swagger.json");

            return options.ReDocEnabled
                ? builder.UseReDoc(c =>
                {
                    c.RoutePrefix = routePrefix;
                    c.SpecUrl = $"{options.Name}/swagger.json";
                })
                : builder.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json", options.Title);
                    c.RoutePrefix = routePrefix;
                });
        }
    }
}