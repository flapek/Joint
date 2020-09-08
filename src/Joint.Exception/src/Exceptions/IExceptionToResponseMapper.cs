namespace Joint.Exception.Exceptions
{
    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(System.Exception exception);
    }
}
