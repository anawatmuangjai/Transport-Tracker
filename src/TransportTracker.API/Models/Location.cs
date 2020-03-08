using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TransportTracker.API.Models
{
    public class Location
    {
        [BsonId]
        public Guid Id { get; set; }
        public int VehicleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
