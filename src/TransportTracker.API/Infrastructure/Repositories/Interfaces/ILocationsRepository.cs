using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportTracker.API.Models;

namespace TransportTracker.API.Infrastructure.Repositories.Interfaces
{
    public interface ILocationsRepository
    {
        Task<Location> GetLocationByIdAsync(Guid id);
        Task<Location> GetCurrentLocationByVehicleIdAsync(int vehicleId);
        Task<List<Location>> GetLocationByVehicleIdAndPeriodTimeAsync(int vehicleId, DateTime startDate, DateTime endDate);
        Task<Location> AddLocationAsync(Location location);
    }
}
