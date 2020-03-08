using System.Threading.Tasks;
using TransportTracker.API.Models;

namespace TransportTracker.API.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string username);
        Task<User> GetUserAsync(string username, string password);
        Task<User> AddUserAsync(User user);
    }
}
