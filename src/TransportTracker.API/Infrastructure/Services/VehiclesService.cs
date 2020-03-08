using System.Collections.Generic;
using System.Threading.Tasks;
using TransportTracker.API.Infrastructure.Repositories.Interfaces;
using TransportTracker.API.Infrastructure.Services.Interfaces;
using TransportTracker.API.Models;
using TransportTracker.API.Models.Dtos;

namespace TransportTracker.API.Infrastructure.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IVehiclesRepository vehiclesRepository;

        public VehiclesService(IVehiclesRepository vehiclesRepository)
        {
            this.vehiclesRepository = vehiclesRepository;
        }

        public async Task<VehicleResponse> GetVehicleAsync(int vehicleId)
        {
            var vehicle = await vehiclesRepository.GetVehicleByVehicleIdAsync(vehicleId);

            if (vehicle == null)
            {
                return null;
            }

            return MapVehicleResponse(vehicle);
        }

        public async Task<List<VehicleResponse>> GetVehicleListAsync()
        {
            var vehicles = await vehiclesRepository.GetVehicleListAsync();

            if (vehicles == null)
            {
                return null;
            }

            var vehicleResponses = new List<VehicleResponse>();

            foreach (var vehicle in vehicles)
            {
                vehicleResponses.Add(MapVehicleResponse(vehicle));
            }

            return vehicleResponses;
        }

        public async Task<VehicleResponse> AddVehicleAsync(VehicleRequest request)
        {
            var vehicle = new Vehicle
            {
                VehicleId = request.VehicleId,
                Number = request.Number,
                Model = request.Model,
                Description = request.Description
            };

            await vehiclesRepository.AddVehicleAsync(vehicle);

            return MapVehicleResponse(vehicle);
        }

        public async Task<bool> VehicleExistsAsync(int vehicleId)
        {
            var vehicle = await vehiclesRepository.GetVehicleByVehicleIdAsync(vehicleId);

            if (vehicle != null)
            {
                return true;
            }

            return false;
        }

        private static VehicleResponse MapVehicleResponse(Vehicle vehicle)
        {
            return new VehicleResponse
            {
                Id = vehicle.Id,
                VehicleId = vehicle.VehicleId,
                Number = vehicle.Number,
                Model = vehicle.Model,
                Description = vehicle.Description
            };
        }
    }
}
