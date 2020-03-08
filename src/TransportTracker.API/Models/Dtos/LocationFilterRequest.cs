using System;
using System.ComponentModel.DataAnnotations;

namespace TransportTracker.API.Models.Dtos
{
    public class LocationFilterRequest
    {
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
