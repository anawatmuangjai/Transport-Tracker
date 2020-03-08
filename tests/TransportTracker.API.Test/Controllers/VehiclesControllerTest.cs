using System;
using System.Collections.Generic;
using System.Text;
using TransportTracker.API.Controllers;
using TransportTracker.API.Infrastructure.Services.Interfaces;
using NSubstitute;
using Xunit;
using System.Threading.Tasks;
using TransportTracker.API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace TransportTracker.API.Test.Controllers
{
    public class VehiclesControllerTest
    {
        private readonly IVehiclesService vehiclesService;
        private readonly VehiclesController vehiclesController;

        public VehiclesControllerTest()
        {
            vehiclesService = Substitute.For<IVehiclesService>();

            vehiclesController = new VehiclesController(vehiclesService);
        }

        [Fact]
        public async Task GetVehicles_AllVehicles_RetrunStatusCodeOK()
        {
            // Arrange
            var vehicleResponse = new List<VehicleResponse>
            {
                new VehicleResponse
                {
                    Id = Guid.NewGuid(),
                    VehicleId = 1,
                    Number = "A-001",
                    Model = "Honda City",
                    Description = "Blue Color"
                }
            };

            vehiclesService.GetVehicleListAsync().Returns(Task.FromResult(vehicleResponse));

            // Act
            var actionResult = await vehiclesController.GetVehicles();
            var objectResult = actionResult as OkObjectResult;

            //Assert
            Assert.NotNull(objectResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetVehicle_ByVehicleId_ReturnStatusCodeOK()
        {
            // Arrange
            var vehicleId = 1;

            var vehicleResponse =  new VehicleResponse
            {
                Id = Guid.NewGuid(),
                VehicleId = 1,
                Number = "A-001",
                Model = "Honda City",
                Description = "Blue Color"
            };

            vehiclesService.GetVehicleAsync(vehicleId).Returns(Task.FromResult(vehicleResponse));

            // Act
            var actionResult = await vehiclesController.GetVehicleById(vehicleId);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            var response = objectResult.Value as VehicleResponse;
            Assert.NotNull(objectResult);
            Assert.Equal(vehicleId, response.VehicleId);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
        }

        [Fact]
        public async Task CreateVehicle_NewVehicleRequest_ReturnStatusCodeOK()
        {
            // Arrange
            var vehicleId = 2;

            var vehicleRequest = new VehicleRequest
            {
                VehicleId = vehicleId,
                Number = "A-002",
                Model = "Honda Civic",
                Description = "Red Color"
            };

            var vehicleResponse = new VehicleResponse
            {
                Id = Guid.NewGuid(),
                VehicleId = vehicleId,
                Number = "A-002",
                Model = "Honda Civic",
                Description = "Red Color"
            };

            vehiclesService.VehicleExistsAsync(Arg.Any<int>()).Returns(false);
            vehiclesService.AddVehicleAsync(Arg.Any<VehicleRequest>()).Returns(Task.FromResult(vehicleResponse));
            vehiclesService.GetVehicleAsync(vehicleId).Returns(Task.FromResult(vehicleResponse));

            // Act
            var actionResult = await vehiclesController.CreateVehicle(vehicleRequest);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, requestResult.StatusCode);
        }
    }
}
