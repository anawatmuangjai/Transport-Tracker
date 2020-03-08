using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TransportTracker.API.Infrastructure.Repositories.Interfaces;
using TransportTracker.API.Infrastructure.Services.Interfaces;
using TransportTracker.API.Models;
using TransportTracker.API.Models.Dtos;

namespace TransportTracker.API.Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository userRepository;
        private readonly AppSettings appSettings;

        public UsersService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            this.userRepository = userRepository;
            this.appSettings = appSettings.Value;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(string username, string password)
        {
            var user = await userRepository.GetUserAsync(username, password);

            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var response = new AuthenticationResponse
            {
                Username = user.Username,
                Role = user.Role,
                Token = tokenHandler.WriteToken(token)
            };

            return response;
        }

        public async Task<AuthenticationResponse> RegisterAsync(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                Role = "Member"
            };

            await userRepository.AddUserAsync(user);
            user.Password = "";

            var response = new AuthenticationResponse
            {
                Username = user.Username,
                Role = user.Role,
            };

            return response;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await userRepository.UserExistsAsync(username);
        }
    }
}
