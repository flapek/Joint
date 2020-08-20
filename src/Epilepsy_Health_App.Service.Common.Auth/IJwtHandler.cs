using Epilepsy_Health_App.Service.Common.Auth.Core;
using Epilepsy_Health_App.Service.Common.Auth.Core.Types;

namespace Epilepsy_Health_App.Service.Common.Auth.Application
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(User user);

        JsonWebRefreshToken CreateRefreshToken(string accessToken);
    }
}