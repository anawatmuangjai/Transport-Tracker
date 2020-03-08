using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TransportTracker.API.Models
{
    public class Vehicle
    {
        [BsonId]
        public Guid Id { get; set; }
        public int VehicleId { get; set; }
        public string Number { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
    }
}
