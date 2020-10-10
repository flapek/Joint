using System.Net;
namespace Joint.Exception.Exceptions
{
    public abstract class DomainException : System.Exception
    {
        public virtual string Code { get; }
        public virtual HttpStatusCode StatusCodes { get; } = HttpStatusCode.BadRequest;

        public DomainException(string message) : base(message)
        {
        }
    }
}
