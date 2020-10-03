using System.Net.Http;
using Joint.HTTP;

namespace Joint.Discovery.Consul.Http
{
    internal sealed class ConsulHttpClient : JointHttpClient, IConsulHttpClient
    {
        public ConsulHttpClient(HttpClient client, HttpClientOptions options)
            : base(client, options)
        {
        }
    }
}