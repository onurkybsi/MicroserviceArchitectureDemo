namespace ApiGateway.Data.Model
{
    public class AuthConfig
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}