# Joint.DB.Redis

## Redis
Adds the [Redis](https://redis.io/) integration with .NET Core based on [IDistributedCache](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.caching.distributed.idistributedcache?view=dotnet-plat-ext-3.1) abstraction.

## Installation
```
dotnet add package Joint.DB.Redis
```

## Dependencies
- [Joint](https://www.nuget.org/packages/Joint/)
- [Microsoft.Extensions.Caching.StackExchangeRedis](https://www.nuget.org/packages/Microsoft.Extensions.Caching.StackExchangeRedis/)

## Usage

Extend ```IJointBuilder``` with ```AddRedis()``` that will register the required services.

```c#
public static IJointBuilder RegisterJoint(this IJointBuilder builder)
{
    builder.AddRedis();
    // Other services.
    
    return builder;
}
```

In order to use Redis integration, inject built-in ```IMemoryCache``` interface.

```c#
public class SomeService
{
    private readonly IMemoryCache _cache;

    public SomeService(IMemoryCache cache)
    {
        _cache = cache;
    }
}
```

## options

- connectionString - connection string e.g. localhost.
- instance - optional prefix, that will be added by default to all the keys.

### appsettings.json

```json
"redis": {
  "connectionString": "localhost",
  "instance": "some-prefix:"
}
```

## Table of Contents
- [Getting started](/src/Joint)
- [Joint.Auth](/src/Joint.Auth)
- [Joint.CQRS.Commands](/src/Joint.CQRS.Commands)
- [Joint.CQRS.Events](/src/Joint.CQRS.Events)
- [Joint.CQRS.Queries](/src/Joint.CQRS.Queries)
- [Joint.DB.Mongo](/src/Joint.DB.Mongo)
- [Joint.DB.Redis](#redis)
- [Joint.Docs.Swagger](/src/Joint.Docs.Swagger)
- [Joint.Logging](/src/Joint.Logging)
- [Joint.WebApi](/src/Joint.WebApi)
