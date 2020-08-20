using SCL.Auth.Core;
using SCL.Auth.Core.Types;

namespace SCL.Auth.Application
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(User user);

        JsonWebRefreshToken CreateRefreshToken(string accessToken);
    }
}