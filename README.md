# Joint [![Build Status](https://travis-ci.org/flapek/Joint.svg?branch=master)](https://travis-ci.org/flapek/Joint)

## Welcome to Joint

Joint is a set of **helper libraries** that can be most of the time (with some exceptions) **used independently of each other** to help you to build your web applications and microservices. **Joint is neither a framework nor a silver bullet.**

Quite opposite, **it’s mostly the set of extensions methods along with additional abstractions** that will help you to deal with common infrastructural concerns.

## Getting started

In order to get started with Joint, simply install the core package:

```
dotnet add package Joint
```

Its sole responsibility is to expose IJointBuilder being used by other packages, which provides fluent API experience, similar to built-in ASP.NET Core IServiceCollection and IApplicationBuilder abstractions.

```c#
public class Program
{
    public static async Task Main(string[] args)
        => await WebHost.CreateDefaultBuilder(args)
            .ConfigureServices(services => services.AddJoint().Build())
            .Configure(app =>
            {
                //Configure the middleware
            })
            .Build()
            .RunAsync();
}
```

Whether you’re using just a Program.cs on its own (yes, you can build your web applications and microservices without a need of having Startup class and AddMvc() along with full UseMvc() middleware) or doing it with a Startup.cs included, just invoke AddJoint() on IServiceCollection instance within the ConfigureServices() method and start using Joint packages.

The core Joint package also registers AppOptions type which contains the application name (and it’s purely optional).

## Options

- name - an optional name of the application.
- displayBanner - display a banner (console output) with the application name during a startup, true by default.

### appsettings.json

```json
"app": {
  "name": "some service",
  "displayBanner": true
}
```