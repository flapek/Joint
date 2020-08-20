using Epilepsy_Health_App.Service.Common.Auth.Core;

namespace Epilepsy_Health_App.Service.Common.Auth.Application
{
    public interface IJwtOptionsBuilder
    {
        IJwtOptionsBuilder WithIssuerSigningKey(string issuerSigningKey);
        IJwtOptionsBuilder WithIssuer(string issuer);
        IJwtOptionsBuilder WithExpiryMinutes(int expiryMinutes);
        IJwtOptionsBuilder WithLifetimeValidation (bool validateLifetime);
        IJwtOptionsBuilder WithAudienceValidation (bool validateAudience);
        IJwtOptionsBuilder WithValidAudience (string validAudience);
        JwtOptions Build();
    }
}