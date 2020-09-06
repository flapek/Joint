# Joint.Auth

JWT authentication.

## JWT

Adds the integration with [JWT][jwt] using an available authentication middleware and system components to validate and grant the access tokens.

## Installation
```
dotnet add package Joint.Auth
```

## Dependencies
- [Joint](https://www.nuget.org/packages/Joint/)
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/)

## Usage

Extend IJointBuilder with AddJwt() that will register the required services.

```c#
public static IJointBuilder RegisterJoint(this IJointBuilder builder)
{
    builder.AddJwt()
    // Other services.

    return builder;
}
```

Then, invoke UseAccessTokenValidator() extension from IApplicationBuilder.

```c#
public static IApplicationBuilder UseJoint(this IApplicationBuilder app)
{
    app.UseAuthentication();
    app.useAuthorization();
    // Other services.

    return app;
}
```

Creating the access tokens can be done by using IJwtHandler interface.

```c#
public class UserService
{
    private readonly IJwtHandler _jwtHandler;

    public UserService(IJwtHandler jwtHandler)
    {
        _jwtHandler = jwtHandler;
    }
    
    public async Task<string> SignInAsync(string email, string password)
    {
        var user = ... //Fetch the user from a custom database
        ValidateCredentials(user, password); //Validate the credentials etc.

        //Generate the token with an optional role and other claims
        var token = _jwtHandler.CreateToken(user.Id, user.Role, user.Claims); 

        return token.AccessToken;
    }
}
```

To blacklist and deactivate the access tokens, use IAccessTokenService and invoke UseAccessTokenValidator() extension. Blacklisted tokens are kept in cache for the period of their expiry.

```c#
public static IApplicationBuilder UseJoint(this IApplicationBuilder app)
{
    app.UseAccessTokenValidator();
    // Other services.

    return app;
}
```

```c#
public class UserService
{
    private readonly IAccessTokenService _accessTokenService;

    public UserService(IAccessTokenService accessTokenService)
    {
        _accessTokenService = accessTokenService;
    }
    
    public async Task SignOutAsync() => await DeactivateCurrentAsync();
}
```

## Options
- secretKey - a secret key used to create the access tokens.
- issuer - a party signing the tokens.
- expiryMinutes - how long the token will remain valid.
- validateLifetime - if true then the lifetime defined in expiryMinutes will be validated.
- validAudience - an audience that can use the access tokens.
- validateAudience - if true then the audience defined in validAudience will be validated.

## Table of Contents
- [Getting started](/src/Joint)
- [Joint.Auth](#jwt)

[jwt]: https://jwt.io/