using System;
using System.Collections.Generic;
using System.Text;
using TransportTracker.API.Infrastructure.Services.Interfaces;
using NSubstitute;
using Xunit;
using TransportTracker.API.Controllers;
using TransportTracker.API.Models.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TransportTracker.API.Test.Controllers
{
    public class UsersControllerTest
    {
        private readonly IUsersService userService;
        private readonly UsersController usersController;

        public UsersControllerTest()
        {
            userService = Substitute.For<IUsersService>();

            usersController = new UsersController(userService);
        }

        [Fact]
        public async Task Authenticate_WithCorrectUsernameAndPassword_ReturnStatusCodeOK()
        {
            // Arrange
            var request = new AuthenticationRequest
            {
                Username = "test",
                Password = "test"
            };

            var response = new AuthenticationResponse
            {
                Username = "test",
                Role = "Admin",
                Token = "AAAAA"
            };

            userService
                .AuthenticateAsync(Arg.Any<string>(), Arg.Any<string>())
                .Returns(Task.FromResult(response));

            // Act
            var actionResult = await usersController.Authenticate(request);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
        }

        [Fact]
        public async Task Authenticate_WithIncorrectUsernameAndPassword_ReturnStatusCodeBadRequest()
        {
            // Arrange
            var request = new AuthenticationRequest
            {
                Username = "test",
                Password = "test"
            };

            userService
                .AuthenticateAsync(Arg.Any<string>(), Arg.Any<string>())
                .Returns(Task.FromResult((AuthenticationResponse)null));

            // Act
            var actionResult = await usersController.Authenticate(request);

            // Assert
            var requestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, requestResult.StatusCode);

        }

        [Fact]
        public async Task Register_WithCorrectRequest__ReturnStatusCodeOK()
        {
            // Arrange
            var request = new AuthenticationRequest
            {
                Username = "test",
                Password = "test"
            };

            var response = new AuthenticationResponse
            {
                Username = "test",
                Role = "Member",
                Token = "AAAAA"
            };

            userService
                .UserExistsAsync(Arg.Any<string>())
                .Returns(false);

            userService
                .RegisterAsync(Arg.Any<string>(), Arg.Any<string>())
                .Returns(Task.FromResult(response));

            // Act
            var actionResult = await usersController.Register(request);

            // Assert
            var requestResult = Assert.IsType<OkResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, requestResult.StatusCode);
        }

        [Fact]
        public async Task Register_WithExsitsUsername_ReturnStatusCodeBadRequest()
        {
            // Arrange
            var request = new AuthenticationRequest
            {
                Username = "test",
                Password = "test"
            };

            var response = new AuthenticationResponse
            {
                Username = "test",
                Role = "Member",
                Token = "AAAAA"
            };

            userService
                .UserExistsAsync(Arg.Any<string>())
                .Returns(true);

            userService
                .RegisterAsync(Arg.Any<string>(), Arg.Any<string>())
                .Returns(Task.FromResult(response));

            // Act
            var actionResult = await usersController.Register(request);

            // Assert
            var requestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, requestResult.StatusCode);
        }
    }
}
