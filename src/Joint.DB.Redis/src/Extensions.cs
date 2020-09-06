using System;
using Joint.Builders;
using Joint.DBRedis.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace Joint.DBRedis
{
    public static class Extensions
    {
        private const string SectionName = "redis";
        private const string RegistryName = "persistence.redis";

        public static IJointBuilder AddRedis(this IJointBuilder builder, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
                sectionName = SectionName;
            
            var options = builder.GetOptions<RedisOptions>(sectionName);
            return builder.AddRedis(options);
        }

        public static IJointBuilder AddRedis(this IJointBuilder builder,
            Func<IRedisOptionsBuilder, IRedisOptionsBuilder> buildOptions)
        {
            var options = buildOptions(new RedisOptionsBuilder()).Build();
            return builder.AddRedis(options);
        }

        public static IJointBuilder AddRedis(this IJointBuilder builder, RedisOptions options)
        {
            if (!builder.TryRegister(RegistryName))
                return builder;

            builder.Services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = options.ConnectionString;
                o.InstanceName = options.Instance;
            });

            return builder;
        }
    }
}