using System.Threading.Tasks;

namespace SCL.Types
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}