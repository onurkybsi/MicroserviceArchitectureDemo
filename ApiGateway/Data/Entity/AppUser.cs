using System;

namespace ApiGateway.Data.Entity.AppUser
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Role { get; set; } = Data.Model.Role.User;
        public string Token { get; set; }
        public DateTime SystemEntryDate { get; }
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
    }
}