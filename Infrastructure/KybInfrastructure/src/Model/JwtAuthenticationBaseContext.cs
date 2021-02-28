namespace KybInfrastructure.Model
{
    public class JwtAuthenticationBaseContext
    {
        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}