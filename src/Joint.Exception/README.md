# Joint.Exception

## Exception
With the usage of Exception package, you can define Exception mapper and send it as a response.

## Installation
```
dotnet add package Joint.Exception
```

## Dependencies
- [Joint](https://www.nuget.org/packages/Joint/)
- [Open.Serialization.Json.Newtonsoft](https://www.nuget.org/packages/Open.Serialization.Json.Newtonsoft/)
- [Open.Serialization.Json.System](https://www.nuget.org/packages/Open.Serialization.Json.System/)
- [Open.Serialization.Json.Utf8Json](https://www.nuget.org/packages/Open.Serialization.Json.Utf8Json/)

## Usage
Create your own exceptions separated by structure(```DomainException```, ```AppException```, ```InfrastructureException```) or if you don't have structure use ```DefaultException```.

### With structure
```c#
public class EmptyAccessTokenException : DomainException
{
  public override string Code { get; } = "empty_access_token";
  public override HttpStatusCode StatusCodes { get; } = HttpStatusCode.BadRequest;

  public EmptyAccessTokenException() : base("Empty access token.")
  {
  }
}
```

```c#
public class UserNotFoundException : AppException
{
  public override string Code { get; } = "user_not_found";
  public override HttpStatusCode StatusCodes { get; } = HttpStatusCode.NotFound;
  public Guid UserId { get; }

  public UserNotFoundException(Guid userId) : base($"User with ID: '{userId}' was not found.")
  {
      UserId = userId;
  }
}
```

### Without structure
```c#
public class UserNotFoundException : DefaultException
{
  public override string Code { get; } = "user_not_found";
  public override HttpStatusCode StatusCodes { get; } = HttpStatusCode.NotFound;
  public Guid UserId { get; }

  public UserNotFoundException(Guid userId) : base($"User with ID: '{userId}' was not found.")
  {
      UserId = userId;
  }
}
```

Create dedicated ```ExceptionToResponseMapper``` class that implements ```IExceptionToResponseMapper``` interface with ```Map()``` method:

```c#
public ExceptionResponse Map(Exception exception)
  => exception switch
  {
    DomainException ex => new ExceptionResponse(new { code = ex.Code, reason = ex.Message }, ex.StatusCodes),
    AppException ex => new ExceptionResponse(new { code = ex.Code, reason = ex.Message }, ex.StatusCodes),
    InfrastructureException ex => new ExceptionResponse(new { code = ex.Code, reason = ex.Message }, ex.StatusCodes),
    _ => new ExceptionResponse(new { code = "error", reason = "There was an error." }, HttpStatusCode.BadRequest)
  };
```

Extend ```IJointBuilder``` with ```AddErrorHandler<T>()``` that will register the required services.

```c#
public static IJointBuilder RegisterJoint(this IJointBuilder builder)
{
    builder.AddErrorHandler<>()
    // Other services.

    return builder;
}
```

Then, invoke ```UseErrorHandler()``` extension from ```IApplicationBuilder```.

```c#
public static IApplicationBuilder UseJoint(this IApplicationBuilder app)
{
    app.UseErrorHandler();
    // Other services.

    return app;
}
```

## Table of Contents
- [Getting started](/src/Joint)
- [Joint.Auth](/src/Joint.Auth)
- [Joint.CQRS.Commands](/src/Joint.CQRS.Commands)
- [Joint.CQRS.Events](/src/Joint.CQRS.Events)
- [Joint.CQRS.Queries](/src/Joint.CQRS.Queries)
- [Joint.DB.Mongo](/src/Joint.DB.Mongo)
- [Joint.DB.Redis](/src/Joint.DB.Redis)
- [Joint.Docs.Swagger](/src/Joint.Docs.Swagger)
- [Joint.Exception](#exception)
- [Joint.Logging](/src/Joint.Logging)
- [Joint.WebApi](/src/Joint.WebApi)
