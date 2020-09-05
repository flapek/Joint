namespace Joint.Auth.Types
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Id { get; set; }
    }
}