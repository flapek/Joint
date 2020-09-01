using Joint.Auth.Types;
using System;

namespace Joint.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, string email);

        JsonWebRefreshToken CreateRefreshToken(string accessToken, Guid userId);
    }
}