using Joint.Builders;
using Joint.Exception.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Joint.Exception
{
    public static class Extensions
    {
        public static IJointBuilder AddErrorHandler<T>(this IJointBuilder builder)
            where T : class, IExceptionToResponseMapper
        {
            builder.Services.AddTransient<ExceptionHandlerMiddleware>();
            
            if (builder.Services.All(s => s.ServiceType != typeof(IExceptionToResponseMapper)))
                builder.Services.AddTransient<IExceptionToResponseMapper, EmptyExceptionToResponseMapper>();
            else
                builder.Services.AddSingleton<IExceptionToResponseMapper, T>();

            return builder;
        }

        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ExceptionHandlerMiddleware>();

        private class EmptyExceptionToResponseMapper : IExceptionToResponseMapper
        {
            public ExceptionResponse Map(System.Exception exception) => null;
        }
    }
}
