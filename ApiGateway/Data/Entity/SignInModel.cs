using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Data.Entity
{
    public class SignInModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
    }
}