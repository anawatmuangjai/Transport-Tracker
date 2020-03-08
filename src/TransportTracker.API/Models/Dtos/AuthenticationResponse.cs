using System;

namespace TransportTracker.API.Models.Dtos
{
    public class AuthenticationResponse
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
