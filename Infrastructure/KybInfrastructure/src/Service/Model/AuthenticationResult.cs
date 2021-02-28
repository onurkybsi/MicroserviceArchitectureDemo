namespace KybInfrastructure.Service
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}