using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Joint.Secrets.Vault.Interfaces
{
    public interface ICertificatesIssuer
    {
        Task<X509Certificate2> IssueAsync();
    }
}