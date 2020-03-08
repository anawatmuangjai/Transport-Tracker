using System;
using System.Collections.Generic;
using System.Text;
using TransportTracker.API.Infrastructure.Services;
using TransportTracker.API.Infrastructure.Services.Interfaces;
using NSubstitute;
using Xunit;
using TransportTracker.API.Infrastructure.Repositories.Interfaces;
using System.Threading.Tasks;
using TransportTracker.API.Models;
using TransportTracker.API.Models.Dtos;

namespace TransportTracker.API.Test.Services
{
    public class LocationsServiceTest
    {
        private readonly IVehiclesRepository vehiclesRepository;
        private readonly ILocationsRepository locationsRepository;
        private readonly LocationsService locationsService;

        public LocationsServiceTest()
        {
            vehiclesRepository = Substitute.For<IVehiclesRepository>();
            locationsRepository = Substitute.For<ILocationsRepository>();

            locationsService = new LocationsService(vehiclesRepository, locationsRepository);
        }

        [Fact]
        public async Task GetLocationByIdAsync_WithLocationIdExists_ReturnLocationResponse()
        {
            // Arrange
            var locationId = new Guid("CFB31112-C780-4535-815A-BB0C93EDD249");

            var location = new Location
            {
                Id = locationId,
                VehicleId = 1,
                Latitude = 13.788571,
                Longitude = 100.538034,
                UpdateDate = new DateTime(2020, 3, 1)
            };

            locationsRepository.GetLocationByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult(location));

            // Act
            var response = await locationsService.GetLocationByIdAsync(locationId);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<LocationResponse>(response);
            Assert.Equal(1, response.VehicleId);
            Assert.Equal(13.788571, response.Latitude);
            Assert.Equal(100.538034, response.Longitude);
        }

        [Fact]
        public async Task GetLocationByIdAsync_WithLocationIdNotExists_ReturnNull()
        {
            // Arrange
            var locationId = new Guid("CFB31112-C780-4535-815A-BB0C93EDD249");

            locationsRepository.GetLocationByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult((Location)null));

            // Act
            var response = await locationsService.GetLocationByIdAsync(locationId);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task GetCurrentLocationByVehicleIdAsync_WithVehicleIdExists_ReturnLocationResponse()
        {
            // Arrange
            var vehicleId = 1;

            var location = new Location
            {
                Id = Guid.NewGuid(),
                VehicleId = 1,
                Latitude = 13.788571,
                Longitude = 100.538034,
                UpdateDate = new DateTime(2020, 3, 1)
            };

            locationsRepository.GetCurrentLocationByVehicleIdAsync(Arg.Any<int>()).Returns(Task.FromResult(location));

            // Act
            var response = await locationsService.GetCurrentLocationByVehicleIdAsync(vehicleId);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<LocationResponse>(response);
            Assert.Equal(1, response.VehicleId);
            Assert.Equal(13.788571, response.Latitude);
            Assert.Equal(100.538034, response.Longitude);
        }

        [Fact]
        public async Task GetCurrentLocationByVehicleIdAsync_WithVehicleIdNotExists_ReturnNull()
        {
            // Arrange
            var vehicleId = 5;

            locationsRepository.GetCurrentLocationByVehicleIdAsync(Arg.Any<int>()).Returns(Task.FromResult((Location)null));

            // Act
            var response = await locationsService.GetCurrentLocationByVehicleIdAsync(vehicleId);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task GetLocationByVehicleIdAndPeriodTimeAsync_WithFilterRequest_ReturnLocationResponse()
        {
            // Arrange
            var locationFilter = new LocationFilterRequest
            {
                VehicleId = 1,
                StartDate = new DateTime(2020, 3, 1),
                EndDate = new DateTime(2020, 3, 2)
            };

            var locations = new List<Location>
            {
                new Location
                {
                    Id = Guid.NewGuid(),
                    VehicleId = 1,
                    Latitude = 13.788571,
                    Longitude = 100.538034,
                    UpdateDate = new DateTime(2020, 3, 1)
                }
            };

            locationsRepository
                .GetLocationByVehicleIdAndPeriodTimeAsync(Arg.Any<int>(), Arg.Any<DateTime>(), Arg.Any<DateTime>())
                .Returns(Task.FromResult(locations));

            // Act
            var response = await locationsService.GetLocationByVehicleIdAndPeriodTimeAsync(locationFilter);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<List<LocationResponse>>(response);

        }

        [Fact]
        public async Task AddLocationAsync_WithLocationRequest_ReturnLocationResponse()
        {
            var locationRequest = new LocationRequest
            {
                VehicleId = 1,
                Latitude = 13.788571,
                Longitude = 100.538034
            };

            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                VehicleId = 1,
                Number = "A-001",
                Model = "Honda City",
                Description = "Blue Color"
            };

            var location = new Location
            {
                Id = Guid.NewGuid(),
                VehicleId = 1,
                Latitude = 13.788571,
                Longitude = 100.538034,
                UpdateDate = new DateTime(2020, 3, 2)
            };

            vehiclesRepository.GetVehicleByVehicleIdAsync(Arg.Any<int>()).Returns(Task.FromResult(vehicle));
            locationsRepository.AddLocationAsync(Arg.Any<Location>()).Returns(Task.FromResult(location));

            // Act
            var response = await locationsService.AddLocationAsync(locationRequest);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<LocationResponse>(response);
            Assert.Equal(1, response.VehicleId);
            Assert.Equal(13.788571, response.Latitude);
            Assert.Equal(100.538034, response.Longitude);
        }
    }
}
