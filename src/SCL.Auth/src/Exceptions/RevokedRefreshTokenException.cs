namespace SCL.Auth.Exceptions
{
    public class RevokedRefreshTokenException : AuthException
    {
        public override string Code { get; } = "revoked_refresh_token";

        public RevokedRefreshTokenException() : base("Revoked refresh token.")
        {
        }
    }
}
