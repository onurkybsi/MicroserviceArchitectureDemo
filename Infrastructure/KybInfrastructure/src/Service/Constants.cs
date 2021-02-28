namespace   KybInfrastructure.Service
{
    public static class Constants
    {
        public static class JwtAuthenticationService
        {
            public static class ErrorMessages
            {
                public const string UserNotExists = "UserNotExists";
                public const string PasswordIsNotCorrect = "UserNotExists";
            }

            public static class UserRole
            {
                public const string Admin = "Admin";
                public const string User = "User";
            }
        }
    }
}