namespace SCL.Auth.Types
{
    public struct User
    {
        public string UserID { get; }
        public string Username { get; }
        
        public User(string userID, string username)
        {
            UserID = userID;
            Username = username;
        }
    }
}
