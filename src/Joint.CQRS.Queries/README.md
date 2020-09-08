# Joint.CQRS.Queries

## Queries
Adds ability to create and process **queries** in the sense of [CQRS](https://martinfowler.com/bliki/CQRS.html).

## Installation
```
dotnet add package Joint.CQRS.Queries
```

## Dependencies
- [Joint](https://www.nuget.org/packages/Joint/)

## Usage

Implement ```IQuery<TResult>``` interface in the selected class:

```c#
public class GetAccount : IQuery<AccountDto>
{
    public Guid Id { get; set; }
}
```

Create dedicated **query handler** class that implements ```IQueryHandler<TQuery, TResult>``` interface with ```HandleAsync()``` method:

```c#
public class GetAccountHandler : IQueryHandler<GetAccount, AccountDto>
{
    public Task<AccountDto> HandleAsync(GetAccount query)
    {
        //put the handling code here
    }
}
```

You can easily register all query handlers in DI container by calling ```AddQueryHandlers()``` method on ```IJointBuilder```:

```c#
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = JointBuilder
        .Create(services)
        .AddQueryHandlers();

    //other registrations    
    return builder.Build();
}
```

Dispatching a particular query object can be also done using Joint package. Start with registering in-memory dispatcher on your ```IJointBuilder``` by calling a ```AddInMemoryEventDispatcher()``` method:

```c#
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    services.AddOpenTracing();

    var builder = JointBuilder
        .Create(services)
        .AddQueryHandlers()
        .AddInMemoryQueryDispatcher();

    //other registrations    
    return builder.Build();
}
```

Then simply inject ```IQueryDispatcher``` into a class and call ```QueryAsync()``` method:

```c#
public class AccountsService
{
    private readonly IQueryDispatcher _dispatcher;

    public AccountsService(IQueryDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    } 

    public Task<AccountDto> GetAccountAsync(Guid id)
        => _dispatcher.QueryAsync(new GetAccount { Id = id });
}
```

## Table of Contents
- [Getting started](/src/Joint)
- [Joint.Auth](/src/Joint.Auth)
- [Joint.CQRS.Commands](/src/Joint.CQRS.Commands)
- [Joint.CQRS.Events](/src/Joint.CQRS.Events)
- [Joint.CQRS.Queries](#queries)
- [Joint.DB.Mongo](/src/Joint.DB.Mongo)
- [Joint.DB.Redis](/src/Joint.DB.Redis)
- [Joint.Docs.Swagger](/src/Joint.Docs.Swagger)
- [Joint.Exception](/src/Joint.Exception)
- [Joint.Logging](/src/Joint.Logging)
- [Joint.WebApi](/src/Joint.WebApi)
