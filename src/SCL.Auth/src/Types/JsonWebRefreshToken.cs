using SCL.Auth.Exceptions;
using System;

namespace SCL.Auth.Types
{
    public class JsonWebRefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedAt { get; private set; }
        public bool Revoked => RevokedAt.HasValue;

        public JsonWebRefreshToken(Guid id, Guid userId, string token, DateTime expires, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Token = token;
            Expires = expires;
            CreatedAt = createdAt;
        }


        public void Revoke(DateTime revokedAt)
        {
            if (Revoked)
            {
                throw new RevokedRefreshTokenException();
            }

            RevokedAt = revokedAt;
        }
    }
}