using System;
using Joint.Builders;
using Joint.CQRS.Queries.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace Joint.CQRS.Queries
{
    public static class Extensions
    {
        public static IJointBuilder AddQueryHandlers(this IJointBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }

        public static IJointBuilder AddInMemoryQueryDispatcher(this IJointBuilder builder)
        {
            builder.Services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
            return builder;
        }
    }
}