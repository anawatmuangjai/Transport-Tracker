using System.ComponentModel.DataAnnotations;

namespace TransportTracker.API.Models.Dtos
{
    public class LocationRequest
    {
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}
