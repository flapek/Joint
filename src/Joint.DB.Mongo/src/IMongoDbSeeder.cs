using System.Threading.Tasks;
using MongoDB.Driver;

namespace Joint.DB.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync(IMongoDatabase database);
    }
}