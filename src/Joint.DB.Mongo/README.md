# Joint.DB.Mongo

## MongoDB
Adds the set of conventions and ease of use for [MongoDB](https://www.mongodb.com/) integration with .NET Core.
![MongoRobo3T][image1]

## Installation
```
dotnet add package Joint.DB.Mongo
```

## Dependencies
- [Joint](https://www.nuget.org/packages/Joint/)
- [Joint.CQRS.Queries](https://www.nuget.org/packages/Joint.CQRS.Queries/)
- [MongoDB.Driver](https://www.nuget.org/packages/MongoDB.Driver/)

## Usage

Extend ```IJointBuilder``` with ```AddMongo()``` that will register the required services.

```c#
public static IJointBuilder RegisterJoint(this IJointBuilder builder)
{
    builder.AddMongo();
    // Other services.
    
    return builder;
}
```

In order to use ```IMongoRepository``` abstraction, invoke ```AddMongoRepository<TDocument, TIdentifiable>("collectionName")``` for each document that you would like to be able to access with this repository abstraction and ensure that document type implements ```IIdentifiable``` interface.

```c#
public static IJointBuilder RegisterJoint(this IJointBuilder builder)
{
    builder.AddMongo()
        .AddMongoRepository<RefreshTokenDocument, Guid>("refreshTokens");
    // Other services.
    
    return builder;
}
```

By using the provided ```IMongoRepository``` you can access helper methods such as ```AddAsync()```, ```BrowseAsync()``` etc. instead of relying on ```IMongoDatabase``` abstraction available via [MongoDB.Driver](https://docs.mongodb.com/drivers/csharp).

```c#
public class SomeService
{
    private readonly IMongoRepository<RefreshTokenDocument, Guid> _repository;

    public SomeService(IMongoRepository<RefreshTokenDocument, Guid> repository)
    {
        _repository = repository;
    }
}
```

## Options

- connectionString - connection string e.g. mongodb://localhost:27017.
- database - database name.
- seed - boolean value, if true then IMongoDbSeeder.SeedAsync() will be invoked (if implemented).
- SetRandomDatabaseSuffix - might be helpful for the integration testing.

### appsettings.json

```json
"mongo": {
  "connectionString": "mongodb://localhost:27017",
  "database": "some-database-name",
  "seed": false,
  "setRandomDatabaseSuffix": false
}
```

## Table of Contents
- [Getting started](/src/Joint)
- [Joint.Auth](/src/Joint.Auth)
- [Joint.CQRS.Commands](/src/Joint.CQRS.Commands)
- [Joint.CQRS.Events](/src/Joint.CQRS.Events)
- [Joint.CQRS.Queries](/src/Joint.CQRS.Queries)
- [Joint.DB.Mongo](#mongoDB)
- [Joint.DB.Redis](/src/Joint.DB.Redis)
- [Joint.Docs.Swagger](/src/Joint.Docs.Swagger)
- [Joint.Logging](/src/Joint.Logging)
- [Joint.WebApi](/src/Joint.WebApi)


[image1]: https://github.com/flapek/Joint/blob/master/Resources/MongoRobo3T.png