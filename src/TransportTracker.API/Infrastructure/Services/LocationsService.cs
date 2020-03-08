using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportTracker.API.Infrastructure.Repositories.Interfaces;
using TransportTracker.API.Infrastructure.Services.Interfaces;
using TransportTracker.API.Models;
using TransportTracker.API.Models.Dtos;

namespace TransportTracker.API.Infrastructure.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly IVehiclesRepository vehiclesRepository;
        private readonly ILocationsRepository locationsRepository;

        public LocationsService(IVehiclesRepository vehiclesRepository, ILocationsRepository locationsRepository)
        {
            this.vehiclesRepository = vehiclesRepository;
            this.locationsRepository = locationsRepository;
        }

        public async Task<LocationResponse> GetLocationByIdAsync(Guid id)
        {
            var location = await locationsRepository.GetLocationByIdAsync(id);

            if (location == null)
            {
                return null;
            }

            return MapLocationResponse(location);
        }

        public async Task<LocationResponse> GetCurrentLocationByVehicleIdAsync(int vehicleId)
        {
            var location = await locationsRepository.GetCurrentLocationByVehicleIdAsync(vehicleId);

            if (location == null)
            {
                return null;
            }

            return MapLocationResponse(location);
        }

        public async Task<IReadOnlyCollection<LocationResponse>> GetLocationByVehicleIdAndPeriodTimeAsync(LocationFilterRequest filterRequest)
        {
            var startDateUtc = filterRequest.StartDate.ToUniversalTime();
            var endDateUtc = filterRequest.EndDate.ToUniversalTime();

            var locations = await locationsRepository.GetLocationByVehicleIdAndPeriodTimeAsync(filterRequest.VehicleId, startDateUtc, endDateUtc);

            var locationResponses = new List<LocationResponse>();

            foreach (var location in locations)
            {
                locationResponses.Add(MapLocationResponse(location));
            }

            return locationResponses;
        }

        public async Task<LocationResponse> AddLocationAsync(LocationRequest request)
        {
            var vehicle = await vehiclesRepository.GetVehicleByVehicleIdAsync(request.VehicleId);

            if (vehicle == null)
            {
                return null;
            }

            var location = new Location
            {
                VehicleId = vehicle.VehicleId,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                UpdateDate = DateTime.UtcNow
            };
               
            await locationsRepository.AddLocationAsync(location);

            return MapLocationResponse(location);
        }

        private static LocationResponse MapLocationResponse(Location location)
        {
            return new LocationResponse
            {
                Id = location.Id,
                VehicleId = location.VehicleId,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                UpdateDate = location.UpdateDate
            };
        }

    }
}
