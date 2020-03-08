using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransportTracker.API.Infrastructure.Repositories.Interfaces;
using TransportTracker.API.Models;

namespace TransportTracker.API.Infrastructure.Repositories
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly ApplicationDbContext context;

        public LocationsRepository(IOptions<DatabaseSettings> settings)
        {
            context = new ApplicationDbContext(settings);
        }

        public async Task<Location> GetLocationByIdAsync(Guid id)
        {
            var filter = Builders<Location>.Filter.Eq("Id", id);
            return await context.Locations.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Location> GetCurrentLocationByVehicleIdAsync(int vehicleId)
        {
            var filter = Builders<Location>.Filter.Eq("VehicleId", vehicleId);
            return await context.Locations.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Location>> GetLocationByVehicleIdAndPeriodTimeAsync(int vehicleId, DateTime startDate, DateTime endDate)
        {
            var filter = Builders<Location>.Filter.Eq("VehicleId", vehicleId) 
                & Builders<Location>.Filter.Gte(x => x.UpdateDate, startDate)
                & Builders<Location>.Filter.Lte(x => x.UpdateDate, endDate);

            return await context.Locations.Find(filter).ToListAsync();
        }

        public async Task<Location> AddLocationAsync(Location location)
        {
            await context.Locations.InsertOneAsync(location);

            return location;
        }
    }
}
