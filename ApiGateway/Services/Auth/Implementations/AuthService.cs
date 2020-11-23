using System;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ApiGateway.Data.Entity.AppUser;
using ApiGateway.Data.Model;

namespace ApiGateway.Services.Auth
{
    public class AuthService : IAuthService
    {
        private const string SaltPointer = "SaltPointer";

        private readonly AuthConfig _authConfig;
        private readonly IAppUserRepo _repo;

        public AuthService(IOptions<AuthConfig> authConfig, IAppUserRepo repo)
        {
            _repo = repo;
            _authConfig = authConfig.Value;
        }

        public AuthResult Authenticate(LoginModel login)
        {
            var user = _repo.GetByFilter(u => u.Email == login.Email);
            if (user is null)
                return new AuthResult
                {
                    IsSuccess = false
                };

            string userHash = user.HashedPassword.Split(SaltPointer)[0];
            string userSalt = user.HashedPassword.Split(SaltPointer)[1];
            if (!EncryptionHelper.ValidateHash(login.Password, userSalt, userHash))
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

            var secretKey = Encoding.ASCII.GetBytes(_authConfig.SecurityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Audience = _authConfig.Audience,
                Issuer = _authConfig.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string createdToken = tokenHandler.WriteToken(token);

            return createdToken;
        }
    }
}