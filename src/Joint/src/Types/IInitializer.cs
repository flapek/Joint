using System.Threading.Tasks;

namespace Joint.Types
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}