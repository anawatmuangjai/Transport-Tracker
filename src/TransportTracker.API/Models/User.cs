using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TransportTracker.API.Models
{
    public class User
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
