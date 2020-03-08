using System.ComponentModel.DataAnnotations;

namespace TransportTracker.API.Models.Dtos
{
    public class AuthenticationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
