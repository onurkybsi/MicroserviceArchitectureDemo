using Infrastructure;

namespace ApiGateway.Model
{
    public class AuthConfig : IAuthConfig
    {
        public byte[] SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}