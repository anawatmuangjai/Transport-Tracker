using System;

namespace TransportTracker.API.Models.Dtos
{
    public class LocationResponse
    {
        public Guid Id { get; set; }
        public int VehicleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
