using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportTracker.API.Models.Dtos;

namespace TransportTracker.API.Infrastructure.Services.Interfaces
{
    public interface ILocationsService
    {
        Task<LocationResponse> GetLocationByIdAsync(Guid id);
        Task<LocationResponse> GetCurrentLocationByVehicleIdAsync(int vehicleId);
        Task<IReadOnlyCollection<LocationResponse>> GetLocationByVehicleIdAndPeriodTimeAsync(LocationFilterRequest locationRequest);
        Task<LocationResponse> AddLocationAsync(LocationRequest vehicle);
    }
}
