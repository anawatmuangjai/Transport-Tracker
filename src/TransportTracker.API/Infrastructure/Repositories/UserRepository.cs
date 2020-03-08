using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using TransportTracker.API.Infrastructure.Repositories.Interfaces;
using TransportTracker.API.Models;

namespace TransportTracker.API.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(IOptions<DatabaseSettings> settings)
        {
            context = new ApplicationDbContext(settings);
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            var filter = Builders<User>.Filter.Eq("Username", username) & Builders<User>.Filter.Eq("Password", password);
            return await context.Users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> AddUserAsync(User user)
        {
            await context.Users.InsertOneAsync(user);

            return user;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            var filter = Builders<User>.Filter.Eq("Username", username);
            var user = await context.Users.Find(filter).FirstOrDefaultAsync();

            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
