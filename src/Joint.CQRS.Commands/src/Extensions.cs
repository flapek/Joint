using System;
using Joint.Builders;
using Joint.CQRS.Commands.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace Joint.CQRS.Commands
{
    public static class Extensions
    {
        public static IJointBuilder AddCommandHandlers(this IJointBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }

        public static IJointBuilder AddInMemoryCommandDispatcher(this IJointBuilder builder)
        {
            builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            return builder;
        }
    }
}