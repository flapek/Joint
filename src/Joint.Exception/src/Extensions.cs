using Joint.Builders;
using Joint.Exception.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Open.Serialization.Json;
using System.Linq;

namespace Joint.Exception
{
    public static class Extensions
    {
        public static IJointBuilder AddErrorHandler<T>(this IJointBuilder builder, IJsonSerializer jsonSerializer = null)
            where T : class, IExceptionToResponseMapper
        {
            builder.Services.AddTransient<ExceptionHandlerMiddleware>();
            var factory = new Open.Serialization.Json.Newtonsoft.JsonSerializerFactory(new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter(true) }
            });
            jsonSerializer = factory.GetSerializer();
            builder.Services.AddSingleton(jsonSerializer);

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
