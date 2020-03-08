using System;
using System.Collections.Generic;
using System.Text;
using TransportTracker.API.Infrastructure.Repositories.Interfaces;
using TransportTracker.API.Infrastructure.Services;
using NSubstitute;
using Xunit;
using System.Threading.Tasks;
using TransportTracker.API.Models;
using TransportTracker.API.Models.Dtos;

namespace TransportTracker.API.Test.Services
{
    public class VehiclesServiceTest
    {
        private readonly IVehiclesRepository vehiclesRepository;
        private readonly VehiclesService vehiclesService;

        public VehiclesServiceTest()
        {
            vehiclesRepository = Substitute.For<IVehiclesRepository>();
            vehiclesService = new VehiclesService(vehiclesRepository);
        }

        [Fact]
        public async Task GetVehicleAsync_WithVehicleIdExists_ReturnVehicleResponse()
        {
            // Arrange
            var vehicleId = 1;

            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                VehicleId = 1,
                Number = "A-001",
                Model = "Honda City",
                Description = "Blue Color"
            };

            vehiclesRepository
                .GetVehicleByVehicleIdAsync(Arg.Any<int>())
                .Returns(Task.FromResult(vehicle));

            // Act
            var response = await vehiclesService.GetVehicleAsync(vehicleId);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<VehicleResponse>(response);
            Assert.Equal(1, response.VehicleId);
            Assert.Equal("A-001", response.Number);
            Assert.Equal("Honda City", response.Model);
            Assert.Equal("Blue Color", response.Description);
        }

        [Fact]
        public async Task GetVehicleAsync_WithVehicleIdNotExists_ReturnNull()
        {
            // Arrange
            var vehicleId = 5;

            vehiclesRepository
                .GetVehicleByVehicleIdAsync(Arg.Any<int>())
                .Returns(Task.FromResult((Vehicle)null));

            // Act
            var response = await vehiclesService.GetVehicleAsync(vehicleId);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task GetVehicleListAsync_WithAllVehicles_ReturnVehicleResponse()
        {
            // Arrange
            var vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    Id = Guid.NewGuid(),
                    VehicleId = 1,
                    Number = "A-001",
                    Model = "Honda City",
                    Description = "Blue Color"
                }
            };

            vehiclesRepository.GetVehicleListAsync().Returns(Task.FromResult(vehicles));

            // Act
            var responses = await vehiclesService.GetVehicleListAsync();

            // Assert
            Assert.NotNull(responses);
            Assert.True(responses.Count > 0);
            Assert.IsType<List<VehicleResponse>>(responses);
        }

        [Fact]
        public async Task AddVehicleAsync_WithVehicleRequest_ReturnVehicleResponse()
        {
            // Arrange
            var request = new VehicleRequest
            {
                VehicleId = 2,
                Number = "A-002",
                Model = "Toyota Vios",
                Description = "Red Color"
            };

            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                VehicleId = 2,
                Number = "A-002",
                Model = "Toyota Vios",
                Description = "Red Color"
            };

            vehiclesRepository.AddVehicleAsync(Arg.Any<Vehicle>()).Returns(Task.FromResult(vehicle));

            // Act
            var response = await vehiclesService.AddVehicleAsync(request);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<VehicleResponse>(response);
            Assert.Equal(2, response.VehicleId);
            Assert.Equal("A-002", response.Number);
            Assert.Equal("Toyota Vios", response.Model);
            Assert.Equal("Red Color", response.Description);
        }
    
        [Fact]
        public async Task VehicleExistsAsync_WithVehicleIdExists_ReturnTrue()
        {
            // Arrange
            var vehicleId = 1;

            vehiclesRepository.GetVehicleByVehicleIdAsync(Arg.Any<int>()).Returns(Task.FromResult(new Vehicle()));

            // Act
            var response = await vehiclesService.VehicleExistsAsync(vehicleId);

            // Assert
            Assert.True(response);
        }

        [Fact]
        public async Task VehicleExistsAsync_WithVehicleIdNotExists_ReturnFalse()
        {
            // Arrange
            var vehicleId = 5;

            vehiclesRepository.GetVehicleByVehicleIdAsync(Arg.Any<int>()).Returns(Task.FromResult((Vehicle)null));

            // Act
            var response = await vehiclesService.VehicleExistsAsync(vehicleId);

            // Assert
            Assert.False(response);
        }
    }
}
