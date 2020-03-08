using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportTracker.API.Models;

namespace TransportTracker.API.Infrastructure.Repositories.Interfaces
{
    public interface IVehiclesRepository
    {
        Task<Vehicle> GetVehicleByIdAsync(Guid id);
        Task<Vehicle> GetVehicleByVehicleIdAsync(int VehicleId);
        Task<List<Vehicle>> GetVehicleListAsync();
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
    }
}
