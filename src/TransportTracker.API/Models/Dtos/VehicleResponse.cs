using System;

namespace TransportTracker.API.Models.Dtos
{
    public class VehicleResponse
    {
        public Guid Id { get; set; }
        public int VehicleId { get; set; }
        public string Number { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
    }
}
