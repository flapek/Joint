using System.Threading.Tasks;
using MongoDB.Driver;

namespace Joint.DB.Mongo
{
    public interface IMongoSessionFactory
    {
        Task<IClientSessionHandle> CreateAsync();
    }
}