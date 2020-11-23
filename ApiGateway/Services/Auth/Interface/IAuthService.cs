using ApiGateway.Data.Model;

namespace ApiGateway.Services.Auth
{
    public interface IAuthService
    {
        AuthResult Authenticate(LoginModel user);
    }
}