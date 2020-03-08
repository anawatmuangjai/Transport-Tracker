using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportTracker.API.Infrastructure.Repositories.Interfaces;
using TransportTracker.API.Models;

namespace TransportTracker.API.Infrastructure.Repositories
{
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly ApplicationDbContext context;

        public VehiclesRepository(IOptions<DatabaseSettings> settings)
        {
            context = new ApplicationDbContext(settings);
        }

        public async Task<Vehicle> GetVehicleByIdAsync(Guid id)
        {
            var filter = Builders<Vehicle>.Filter.Eq("Id", id);
            return await context.Vehicles.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Vehicle> GetVehicleByVehicleIdAsync(int vehicleId)
        {
            var filter = Builders<Vehicle>.Filter.Eq("VehicleId", vehicleId);
            return await context.Vehicles.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Vehicle>> GetVehicleListAsync()
        {
            return await context.Vehicles.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            await context.Vehicles.InsertOneAsync(vehicle);
            return vehicle;
        }
    }
}
