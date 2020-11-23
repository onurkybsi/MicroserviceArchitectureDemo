namespace ApiGateway.Services.Auth
{
    public interface IAuthService
    {
        AuthResult Authenticate(ILoginModel user);
    }
}