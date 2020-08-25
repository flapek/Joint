using System;

namespace SCL
{
    public class ServiceId : IServiceId
    {
        public string Id { get; } = $"{Guid.NewGuid():N}";
    }
}