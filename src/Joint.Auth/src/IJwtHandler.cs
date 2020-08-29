using Joint.Auth.Types;
using System;

namespace Joint.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(User user);

        JsonWebRefreshToken CreateRefreshToken(string accessToken, Guid userId);
    }
}