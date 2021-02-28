using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace KybInfrastructure.Service
{
    public class JwtAuthenticationService
    {
        private readonly JwtAuthenticationContext _context;

        public JwtAuthenticationService(JwtAuthenticationContext context)
        {
            _context = context;
        }

        public async Task<AuthenticationResult> Authenticate(SignInModel signInModel)
        {
            var user = await _context.GetUserAction(signInModel);

            if (user is null)
                return new AuthenticationResult
                {
                    IsAuthenticated = false,
                    Message = Constants.JwtAuthenticationService.ErrorMessages.UserNotExists
                };

            if (!VerifyUser(signInModel.Password, user.HashedPassword))
            {
                return new AuthenticationResult
                {
                    IsAuthenticated = false,
                    Message = Constants.JwtAuthenticationService.ErrorMessages.PasswordIsNotCorrect
                };
            }

            string createdToken = CreateToken(user);

            user.Token = createdToken;

            return new AuthenticationResult
            {
                IsAuthenticated = true,
                Token = createdToken
            };
        }

        private bool VerifyUser(string loginPassword, string userHashedPassword)
        {
            string userHash = userHashedPassword.Split(EncryptionHelper.SaltPointer)[0];
            string userSalt = userHashedPassword.Split(EncryptionHelper.SaltPointer)[1];

            return !EncryptionHelper.VerifyHashed(loginPassword, userSalt, userHash) ? false : true;
        }

        private string CreateToken(IUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),

                Audience = _context.Audience,
                Issuer = _context.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_context.SecurityKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string createdToken = tokenHandler.WriteToken(token);

            return createdToken;
        }
    }
}