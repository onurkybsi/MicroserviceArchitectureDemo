using System;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ApiGateway.Data.AppUser;
using Infrastructure;

namespace ApiGateway.Services.Auth
{
    public class AuthService : IAuthService
    {
        private static string SaltPointer = Startup.StaticConfiguration["SaltPointer"];

        private readonly IAuthConfig _authConfig;
        private readonly IAppUserRepo _repo;

        public AuthService(IAuthConfig authConfig, IAppUserRepo repo)
        {
            _repo = repo;
            _authConfig = authConfig;
        }

        // TO-DO: Bu metot EncryptionHelper a bağımlı durumda şuan saltPointer üzerinden. Bunu düşün.
        public AuthResult Authenticate(ILoginModel login)
        {
            var user = _repo.Get(u => u.Email == login.Email);
            if (user is null)
                return new AuthResult
                {
                    IsSuccess = false
                };

            string userHash = user.HashedPassword.Split(SaltPointer)[0];
            string userSalt = user.HashedPassword.Split(SaltPointer)[1];
            if (!EncryptionHelper.VerifyHashed(login.Password, userSalt, userHash))
            {
                return new AuthResult
                {
                    IsSuccess = false
                };
            }

            string createdToken = CreateToken(user);

            user.Token = createdToken;
            _repo.Update(user);

            return new AuthResult
            {
                IsSuccess = true,
                Token = createdToken
            };
        }

        private string CreateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Audience = _authConfig.Audience,
                Issuer = _authConfig.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(200),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_authConfig.SecurityKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string createdToken = tokenHandler.WriteToken(token);

            return createdToken;
        }
    }
}