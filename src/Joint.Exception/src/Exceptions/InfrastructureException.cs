using System.Net;

namespace Joint.Exception.Exceptions
{
    public abstract class InfrastructureException : System.Exception
    {
        public virtual string Code { get; }
        public virtual HttpStatusCode StatusCodes { get; } = HttpStatusCode.InternalServerError;

        public InfrastructureException(string message) : base(message)
        {
        }
    }
}
