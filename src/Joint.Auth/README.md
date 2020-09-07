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

:warning: **If you are using mobile browser**: Be very careful here!

## Options
- allowAnonymousEndpoints - list of anonymous endpoints used to send request to specific routes.
- certificate - if is not null algorithm use specific certificate else use symmetric key.
    - location - certyficate location.
    - rawData - a byte array containing data from an X.509 certificate.
    - password - the password required to access the X.509 certificate data
- issuer - if this value is not null, a { iss, 'issuer' } claim will be added.
- issuerSigningKey - it's used to set key for symmetric security key.
- authority - gets or sets the Authority to use when making OpenIdConnect calls.
- challenge - gets or sets the challenge to put in the "WWW-Authenticate" header.
- metadataAddress - gets or sets the discovery endpoint for obtaining metadata.
- expiryMinutes - it's minutes which is added to actual time 
- expiry - it's total milliseconds which is added to actual time (```TimeSpan```).
- validAudience - Gets or sets a string that represents a valid audience that will be used to check against the token's audience.
- validAudiences - gets or sets the System.Collections.Generic.IEnumerable`1 that contains valid audiences that will be used to check against the token's audience.
- validIssuer - gets or sets a System.String that represents a valid issuer that will be used to check against the token's issuer.
- validIssuers - gets or sets the System.Collections.Generic.IEnumerable`1 that contains valid issuers that will be used to check against the token's issuer.
- authenticationType - gets or sets the AuthenticationType when creating a System.Security.Claims.ClaimsIdentity.
- nameClaimType - gets or sets a System.String that defines the System.Security.Claims.ClaimsIdentity.NameClaimType.
- roleClaimType - gets or sets the System.String that defines the System.Security.Claims.ClaimsIdentity.RoleClaimType.
- saveToken - defines whether the bearer token should be stored in the Microsoft.AspNetCore.Authentication.AuthenticationProperties after a successful authorization. **Default true.**
- saveSignInToken - gets or sets a boolean to control if the original token should be saved after the security token is validated. 
- requireAudience - gets or sets a value indicating whether SAML tokens must have at least one AudienceRestriction. **Default true.**
- requireHttpsMetadata - gets or sets if HTTPS is required for the metadata address or authority. This should be disabled only in development environments. **Default true.**
- requireExpirationTime - gets or sets a value indicating whether tokens must have an 'expiration' value. **Default true.**
- requireSignedTokens - gets or sets a value indicating whether a Microsoft.IdentityModel.Tokens.SecurityToken can be considered valid if not signed. **Default true.**
- validateActor - gets or sets a value indicating if an actor token is detected, whether it should be validated.
- validateAudience - gets or sets a boolean to control if the audience will be validated during token validation. **Default true.**
- validateIssuer - gets or sets a boolean to control if the issuer will be validated during token validation. **Default true.**
- validateLifetime - gets or sets a boolean to control if the lifetime will be validated during token validation. **Default true.**
- validateTokenReplay - gets or sets a boolean to control if the token replay will be validated during token validation.
- validateIssuerSigningKey - gets or sets a boolean that controls if validation of the Microsoft.IdentityModel.Tokens.SecurityKey that signed the securityToken is called.
- refreshOnIssuerKeyNotFound - gets or sets if a metadata refresh should be attempted after a SecurityTokenSignatureKeyNotFoundException. This allows for automatic recovery in the event of a signature key rollover.  **Default true.**
- includeErrorDetails - defines whether the token validation errors should be returned to the caller. Enabled by default, this option can be disabled to prevent the JWT handler from returning an error and an error_description in the WWW-Authenticate header. **Default true.**

### appsettings.json

```json
"jwt": {
    "certificate": {
      "location": "certs/localhost.pfx",
      "password": "test",
      "rawData": ""
    },
    "issuerSigningKey": "eiquief5phee9pazo0Faegaez9gohThailiur5woy2befiech1oarai4aiLi6ahVecah3ie9Aiz6Peij",
    "expiryMinutes": 60,
    "issuer": "pacco",
    "validateAudience": false,
    "validateIssuer": false,
    "validateLifetime": true,
    "allowAnonymousEndpoints": ["/sign-in", "/sign-up"]
}
```

## Table of Contents
- [Getting started](/src/Joint)
- [Joint.Auth](#jwt)
- [Joint.CQRS.Commands](/src/Joint.CQRS.Commands)
- [Joint.CQRS.Events](/src/Joint.CQRS.Events)
- [Joint.CQRS.Queries](/src/Joint.CQRS.Queries)
- [Joint.DB.Mongo](/src/Joint.DB.Mongo)
- [Joint.DB.Redis](/src/Joint.DB.Redis)
- [Joint.Docs.Swagger](/src/Joint.Docs.Swagger)
- [Joint.Logging](/src/Joint.Logging)
- [Joint.WebApi](/src/Joint.WebApi)


[jwt]: https://jwt.io/