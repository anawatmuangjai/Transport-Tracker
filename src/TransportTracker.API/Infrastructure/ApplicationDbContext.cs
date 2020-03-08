using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TransportTracker.API.Models;

namespace TransportTracker.API.Infrastructure
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase database = null;

        public ApplicationDbContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                database = client.GetDatabase(settings.Value.DatabaseName);
            }
        }

        public IMongoCollection<Vehicle> Vehicles
        {
            get
            {
                return database.GetCollection<Vehicle>("Vehicles");
            }
        }

        public IMongoCollection<Location> Locations
        {
            get 
            {
                return database.GetCollection<Location>("Locations"); 
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return database.GetCollection<User>("Users");
            }
        }
    }
}
