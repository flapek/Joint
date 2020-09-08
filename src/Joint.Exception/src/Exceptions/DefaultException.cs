using System.Net;

namespace Joint.Exception.Exceptions
{
    public abstract class DefaultException : System.Exception
    {
        public virtual string Code { get; }
        public virtual HttpStatusCode StatusCodes { get; }

        public DefaultException(string message) : base(message)
        {
        }
    }
}
