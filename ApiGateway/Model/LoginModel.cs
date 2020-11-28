using System.ComponentModel.DataAnnotations;
using ApiGateway.Services.Auth;

namespace ApiGateway.Model
{
    public class LoginModel : ILoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}