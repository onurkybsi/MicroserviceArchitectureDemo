using System;

namespace ApiGateway.Data.AppUser
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Role { get; set; } = ApiGateway.Data.Entity.Role.User;
        public string Token { get; set; }
    }
}