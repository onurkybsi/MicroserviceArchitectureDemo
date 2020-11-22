using ApiGateway.Data.AppUser;
using ApiGateway.Data.Entity;

namespace ApiGateway.Services.Auth
{
    public interface IAuthService
    {
        AuthResult Authenticate(LoginModel user);
    }
}