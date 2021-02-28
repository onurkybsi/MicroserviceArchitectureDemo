using KybInfrastructure.Model;

namespace KybInfrastructure.Host
{
    public class JwtAuthenticationContext : JwtAuthenticationBaseContext
    {
        public Environment Environment { get; set; }
    }
}