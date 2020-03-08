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
    public class LocationsControllerTest
    {
        private readonly ILocationsService locationsService;
        private readonly LocationsController locationsController;

        public LocationsControllerTest()
        {
            locationsService = Substitute.For<ILocationsService>();

            locationsController = new LocationsController(locationsService);
        }

        [Fact]
        public async Task GetLocation_WithLocationByIdExists_ReturnStatusCodeOK()
        {
            // Arrange
            var locationId = new Guid("CFB31112-C780-4535-815A-BB0C93EDD249");

            var vehicleResponse = new LocationResponse
            {
                Id = locationId,
                VehicleId = 1,
                Latitude = 13.788571,
                Longitude = 100.538034,
            };

            locationsService.GetLocationByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult(vehicleResponse));

            // Act
            var actionResult = await locationsController.GetLocationById(locationId);
            var objectResult = actionResult as OkObjectResult;

            //Assert
            var response = objectResult.Value as LocationResponse;
            Assert.NotNull(response);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(locationId, response.Id);
            Assert.Equal(1, response.VehicleId);
            Assert.Equal(13.788571, response.Latitude);
            Assert.Equal(100.538034, response.Longitude);
        }

        [Fact]
        public async Task GetCurrentLocation_WithVehicleIdExists_ReturnStatusCodeOK()
        {
            // Arrange
            var vehicleId = 1;
            var locationId = new Guid("CFB31112-C780-4535-815A-BB0C93EDD249");

            var vehicleResponse = new LocationResponse
            {
                Id = locationId,
                VehicleId = 1,
                Latitude = 13.788571,
                Longitude = 100.538034,
            };

            locationsService.GetCurrentLocationByVehicleIdAsync(Arg.Any<int>()).Returns(Task.FromResult(vehicleResponse));

            // Act
            var actionResult = await locationsController.GetCurrentLocationByVehicleId(vehicleId);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            var response = objectResult.Value as LocationResponse;
            Assert.NotNull(response);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(locationId, response.Id);
            Assert.Equal(1, response.VehicleId);
            Assert.Equal(13.788571, response.Latitude);
            Assert.Equal(100.538034, response.Longitude);
        }

        [Fact]
        public async Task GetCurrentLocation_WithVehicleIdAndPeriodTime_ReturnStatusCodeOK()
        {
            // Arrange
            var locationFilter = new LocationFilterRequest
            {
                VehicleId = 1,
                StartDate = new DateTime(2020, 3, 1),
                EndDate = new DateTime(2020, 3, 2)
            };

            var locationId = new Guid("CFB31112-C780-4535-815A-BB0C93EDD249");

            var locationResponses = new List<LocationResponse>
            {
                new LocationResponse
                {
                    Id = locationId,
                    VehicleId = 1,
                    Latitude = 13.788571,
                    Longitude = 100.538034,
                }
            };

            locationsService
                .GetLocationByVehicleIdAndPeriodTimeAsync(Arg.Any<LocationFilterRequest>())
                .Returns(Task.FromResult<IReadOnlyCollection<LocationResponse>>(locationResponses));

            // Act
            var actionResult = await locationsController.GetLocationByVehicleIdAndPeriodTime(locationFilter);
            var objectResult = actionResult as OkObjectResult;

            //Assert
            Assert.NotNull(objectResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
        }

        [Fact]
        public async Task AddLocation_NewLocationRequest_ReturnStatusCodeCreatedAtExpectedRoutName()
        {
            // Arrange
            var locationRequest = new LocationRequest
            {
                VehicleId = 1,
                Latitude = 13.788571,
                Longitude = 100.538034
            };

            var locationResponse = new LocationResponse
            {
                Id = Guid.NewGuid(),
                VehicleId = 1,
                Latitude = 13.788571,
                Longitude = 100.538034,
            };

            locationsService
                .AddLocationAsync(Arg.Any<LocationRequest>())
                .Returns(Task.FromResult(locationResponse));

            // Act
            var actionResult = await locationsController.AddLocation(locationRequest);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, requestResult.StatusCode);
        }
    }
}
