using System.Threading.Tasks;
using TransportTracker.API.Models.Dtos;

namespace TransportTracker.API.Infrastructure.Services.Interfaces
{
    public interface IUsersService
    {
        Task<bool> UserExistsAsync(string username);
        Task<AuthenticationResponse> AuthenticateAsync(string username, string password);
        Task<AuthenticationResponse> RegisterAsync(string username, string password);
    }
}
