namespace SCL.Auth.Core.Types
{
    public struct User
    {
        public string UserID { get; }
        public string Username { get; }
        public string ServerToken { get; }
        public string ServerName { get; }
        
        public User(string userID, string username, string serverToken, string serverName)
        {
            UserID = userID;
            Username = username;
            ServerToken = serverToken;
            ServerName = serverName;
        }
    }
}
