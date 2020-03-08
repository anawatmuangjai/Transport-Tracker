using System.Collections.Generic;
using System.Threading.Tasks;
using TransportTracker.API.Models.Dtos;

namespace TransportTracker.API.Infrastructure.Services.Interfaces
{
    public interface IVehiclesService
    {
        Task<VehicleResponse> GetVehicleAsync(int vehicleId);
        Task<List<VehicleResponse>> GetVehicleListAsync();
        Task<bool> VehicleExistsAsync(int vehicleId);
        Task<VehicleResponse> AddVehicleAsync(VehicleRequest vehicle);
    }
}
