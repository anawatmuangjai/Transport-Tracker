using System.ComponentModel.DataAnnotations;

namespace TransportTracker.API.Models.Dtos
{
    public class VehicleRequest
    {
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Model { get; set; }
        public string Description { get; set; }
    }
}
