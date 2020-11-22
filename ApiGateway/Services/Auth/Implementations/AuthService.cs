using System;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ApiGateway.Data.Entity;
using ApiGateway.Data.AppUser;
using ApiGateway.Infrastructure;

namespace ApiGateway.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JWTAuthConfig _tokenConfig;
        private readonly IAppUserRepo _repo;

        public AuthService(IOptions<JWTAuthConfig> tokenConfig, IAppUserRepo repo)
        {
            _repo = repo;
            _tokenConfig = tokenConfig.Value;
        }

        public AuthResult Authenticate(LoginModel login)
        {
            var user = _repo.GetByFilter(u => u.Email == login.Email);
            if (user is null) return null;

            string userHash = user.HashedPassword.Split("saltis")[0];
            string userSalt = user.HashedPassword.Split("saltis")[1];
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

            var secretKey = Encoding.ASCII.GetBytes(_tokenConfig.SecurityKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Audience = _tokenConfig.Audience,
                Issuer = _tokenConfig.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(0.5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string createdToken = tokenHandler.WriteToken(token);

            return createdToken;
        }
    }
}