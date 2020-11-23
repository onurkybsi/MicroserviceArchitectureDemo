using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Services.Auth
{
    public interface ILoginModel
    {
        [Required]
        string Email { get; set; }
        [Required]
        string Password { get; set; }
    }
}