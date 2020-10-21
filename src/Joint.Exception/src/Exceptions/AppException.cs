using System.Net;

namespace Joint.Exception.Exceptions
{
    public abstract class AppException : System.Exception
    {
        public virtual string Code { get; }
        public virtual HttpStatusCode StatusCodes { get; } = HttpStatusCode.BadRequest;

        public AppException(string message) : base(message)
        {
        }
    }
}
