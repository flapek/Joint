# Joint.Docs.Swagger

## Docs
Adds API documentation using [Swagger](https://swagger.io/docs/).

## Installation
```
dotnet add package Joint.Docs.Swagger
```

## Dependencies
- [Joint](https://www.nuget.org/packages/Joint/)
- [Swashbuckle.AspNetCore.Swagger](https://www.nuget.org/packages/Swashbuckle.AspNetCore.Swagger/)
- [Swashbuckle.AspNetCore.SwaggerGen](https://www.nuget.org/packages/Swashbuckle.AspNetCore.SwaggerGen/)
- [Swashbuckle.AspNetCore.SwaggerUi](https://www.nuget.org/packages/Swashbuckle.AspNetCore.SwaggerUi/)

## Usage

Inside ```Startup.cs``` extend ```IJointBuilder``` with ```AddSwaggerDocs()``` and ```IApplicationBuilder``` with ```UseSwaggerDocs()```:

```c#
public IServiceProvider ConfigureServices(this IServiceCollection services)
{
    var builder = JointBuilder
        .Create(services)
        .AddSwaggerDocs(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    //other registrations    
    return builder.Build();
}

public void Configure(this IApplicationBuilder app)
{
    app.UseSwaggerDocs();
}
```
Having this done your docs should be available at **http://{host}:{port}/{routePrefix}**

## Options

- Enabled - determines whether documentation is enabled
- Name - name of the documentation
- Title - title of the documentation
- Version - version of the documentation
- RoutePrefix - endpoint at which the documentation is going to be available
- IncludeSecurity - determines whether documentation security (via JWT) is going to be activated
- ContactName - name of person which we can contact
- ContactEmail - email of person which we can contact

### appsettings.json

```json
"swagger": {
    "enabled": true,
    "name": "v1",
    "title": "API",
    "version": "v1",
    "routePrefix": "docs",
    "includeSecurity": true,
    "contactName": "some-name",
    "contactEmail": "some.email@example.com"
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
