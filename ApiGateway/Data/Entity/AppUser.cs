using System;
using Infrastructure.Data;
using Newtonsoft.Json;

namespace ApiGateway.Data.AppUser
{
    public class AppUser : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string HashedPassword { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime SystemEntryDate { get; }
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
    }
}