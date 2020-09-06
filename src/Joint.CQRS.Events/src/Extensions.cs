using System;
using Joint.Builders;
using Joint.CQRS.Events.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace Joint.CQRS.Events
{
    public static class Extensions
    {
        public static IJointBuilder AddEventHandlers(this IJointBuilder builder)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }
        
        public static IJointBuilder AddInMemoryEventDispatcher(this IJointBuilder builder)
        {
            builder.Services.AddSingleton<IEventDispatcher, EventDispatcher>();
            return builder;
        }
    }
}