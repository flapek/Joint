using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Joint.Discovery.Consul.Models;

namespace Joint.Discovery.Consul
{
    public interface IConsulService
    {
        Task<HttpResponseMessage> RegisterServiceAsync(ServiceRegistration registration);
        Task<HttpResponseMessage> DeregisterServiceAsync(string id);
        Task<IDictionary<string, ServiceAgent>> GetServiceAgentsAsync(string service = null);
    }
}