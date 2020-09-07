# Joint.CQRS.Events

## Events
Adds ability to create and process **events** in the sense of [CQRS](https://martinfowler.com/bliki/CQRS.html).

## Installation
```
dotnet add package Joint.CQRS.Envents
```

## Dependencies
- [Joint](https://www.nuget.org/packages/Joint/)

## Usage

Implement ```IEvent``` or ```IRejectedEvent``` (marker) interface in the selected class. Since the event represents something that already happened, you should follow the convention:

- keep all the events  **immutable**
- name of your events should be **past tense**

```c#
public class AccountCreated : IEvent
{
    public Guid Id { get; }

    public AccountCreated(id)
    {
        Id = id;
    }
}
```

Create dedicated **event handler** class that implements ```IEventHandler<TEvent>``` interface with ```HandleAsync()``` method:

```c#
public class AccountCreatedHandler : IEventHandler<AccountCreated>
{
    public Task HandleAsync(AccountCreated @event)
    {
        //put the handling code here
    }
}
```

You can easily register all event handlers in DI container by calling ```AddEventHandlers()``` method on ```IJointBuilder```:

```c#
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = JointBuilder
        .Create(services)
        .AddEventHandlers();

    //other registrations    
    return builder.Build();
}
```

Dispatching a particular event object can be also done using Joint package. Start with registering in-memory dispatcher on your ```IJointBuilder``` by calling a ```AddInMemoryEventDispatcher()``` method:

```c#
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    services.AddOpenTracing();

    var builder = JointBuilder
        .Create(services)
        .AddCommandHandlers()
        .AddInMemoryEventDispatcher();

    //other registrations    
    return builder.Build();
}
```

Then simply inject ```IEventDispatcher``` into a class and call ```PublishAsync()``` method:

```c#
public class AccountsService
{
    private readonly IEventDispatcher _dispatcher;

    public AccountsService(IEventDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    } 

    public Task PostProcessAccountCreation(AccountCreated @event)
        => _dispatcher.PublishAsync(@event);
}
```

## Table of Contents
- [Getting started](/src/Joint)
- [Joint.Auth](/src/Joint.Auth)
- [Joint.CQRS.Commands](/src/Joint.CQRS.Commands)
- [Joint.CQRS.Events](#events)
- [Joint.CQRS.Queries](/src/Joint.CQRS.Queries)
- [Joint.DB.Mongo](/src/Joint.DB.Mongo)
- [Joint.DB.Redis](/src/Joint.DB.Redis)
- [Joint.Docs.Swagger](/src/Joint.Docs.Swagger)
- [Joint.Logging](/src/Joint.Logging)
- [Joint.WebApi](/src/Joint.WebApi)
