using System;
using System.Threading.Tasks;
using KybInfrastructure.Model;

namespace KybInfrastructure.Service
{
    public class JwtAuthenticationContext : JwtAuthenticationBaseContext
    {
        public Func<SignInModel, Task<IUser>> GetUserAction { get; set; }
    }
}