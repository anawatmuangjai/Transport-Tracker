using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSubstitute;
using TransportTracker.API.Infrastructure.Repositories.Interfaces;
using TransportTracker.API.Infrastructure.Services;
using TransportTracker.API.Models;
using TransportTracker.API.Models.Dtos;
using Xunit;

namespace TransportTracker.API.Test.Services
{
    public class UsersServiceTest
    {
        private readonly IUserRepository userRepository;
        private readonly IOptions<AppSettings> appSettings;
        private readonly UsersService usersService;

        public UsersServiceTest()
        {
            userRepository = Substitute.For<IUserRepository>();
            appSettings = Options.Create(new AppSettings { Secret = "123456" });

            usersService = new UsersService(userRepository, appSettings);
        }

        [Fact]
        public async Task RegisterAsync_NewUser_ReturnCorrectResponse()
        {
            // Arrange
            var username = "test";
            var password = "test";

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Password = password,
                Role = "Member"
            };

            userRepository
                .AddUserAsync(Arg.Any<User>())
                .Returns(Task.FromResult(user));

            // Act
            var response = await usersService.RegisterAsync(username, password);

            // Assert
            Assert.NotNull(response);
            Assert.Equal("test", response.Username);
            Assert.Equal("Member", response.Role);
        }

        [Fact]
        public async Task UserExistsAsync_WithUsernameNotExists_ReturnFalse()
        {
            // Arrange
            var username = "test";

            userRepository
                .UserExistsAsync(Arg.Any<string>())
                .Returns(Task.FromResult(false));

            // Act
            var response = await usersService.UserExistsAsync(username);

            // Assert
            Assert.False(response);
        }
    }
}
