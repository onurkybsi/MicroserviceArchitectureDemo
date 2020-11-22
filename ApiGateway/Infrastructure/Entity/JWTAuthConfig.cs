namespace ApiGateway.Infrastructure
{
    public class JWTAuthConfig
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}