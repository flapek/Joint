using Joint.Builders;
using Joint.Exception.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Open.Serialization.Json;

namespace Joint.Exception
{
    public static class Extensions
    {
        public static IJointBuilder AddErrorHandler<T>(this IJointBuilder builder, IJsonSerializer jsonSerializer = null)
            where T : class, IExceptionToResponseMapper
        {
            builder.Services.AddTransient<ExceptionHandlerMiddleware>();
            if (jsonSerializer is null)
            {
                var factory = new Open.Serialization.Json.Newtonsoft.JsonSerializerFactory(new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = { new StringEnumConverter(true) }
                });
                jsonSerializer = factory.GetSerializer();
            }

            if (jsonSerializer.GetType().Namespace?.Contains("Newtonsoft") == true)
            {
                builder.Services.Configure<KestrelServerOptions>(o => o.AllowSynchronousIO = true);
                builder.Services.Configure<IISServerOptions>(o => o.AllowSynchronousIO = true);
            }

            builder.Services.AddSingleton(jsonSerializer);

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
