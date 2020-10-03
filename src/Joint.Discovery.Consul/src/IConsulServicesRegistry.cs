using System.Threading.Tasks;
using Joint.Discovery.Consul.Models;

namespace Joint.Discovery.Consul
{
    public interface IConsulServicesRegistry
    {
        Task<ServiceAgent> GetAsync(string name);
    }
}