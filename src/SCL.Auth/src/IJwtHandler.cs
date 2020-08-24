using SCL.Auth.Types;
using System;

namespace SCL.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(User user);

        JsonWebRefreshToken CreateRefreshToken(string accessToken, Guid userId);
    }
}