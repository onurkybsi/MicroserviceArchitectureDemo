namespace Infrastructure.Model
{
    public interface IAuthConfig
    {
        byte[] SecurityKey { get; }
        string Issuer { get; }
        string Audience { get; }
    }
}