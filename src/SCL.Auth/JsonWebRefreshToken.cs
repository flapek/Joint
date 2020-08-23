using System;

namespace SCL.Auth.Core
{
    public class JsonWebRefreshToken
    {
        public string Token { get; set; }
        public long Expires { get; set; }
        public DateTime Created { get; set; }
    }
}