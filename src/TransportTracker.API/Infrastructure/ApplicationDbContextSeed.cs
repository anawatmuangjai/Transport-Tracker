using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using TransportTracker.API.Models;

namespace TransportTracker.API.Infrastructure
{
    public class ApplicationDbContextSeed
    {
        private static ApplicationDbContext context;

        public static async Task SeedAsync(IApplicationBuilder applicationBuilder)
        {
            var config = applicationBuilder.ApplicationServices.GetRequiredService<IOptions<DatabaseSettings>>();

            context = new ApplicationDbContext(config);

            var filter = Builders<User>.Filter.Eq("Username", "admin");
            var user = await context.Users.Find(filter).FirstOrDefaultAsync();
            if (user == null)
            {
                await SetDefaultUser();
            }

            var location = await context.Locations.Find(x => true).FirstOrDefaultAsync();
            if (location != null)
            {
                await SetIndexes();
            }
        }

        static async Task SetDefaultUser()
        {
            var user = new User
            {
                Username = "admin",
                Password = "admin",
                Role = "Admin"
            };

            await context.Users.InsertOneAsync(user);
        }

        static async Task SetIndexes()
        {
            var indexKeysDefinition = Builders<Location>.IndexKeys.Combine(
                    Builders<Location>.IndexKeys.Ascending(x => x.VehicleId),
                    Builders<Location>.IndexKeys.Ascending(x => x.UpdateDate));

            var indexModel = new CreateIndexModel<Location>(indexKeysDefinition);
            await context.Locations.Indexes.CreateOneAsync(indexModel);
        }
    }
}
