using System.Threading.Tasks;
using ApiGateway.Data.Entity.AppUser;
using ApiGateway.Data.Model;
using ApiGateway.Services.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiGateway.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _accountService;
        private readonly IAppUserRepo _repo;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService accountService, IAppUserRepo repo, ILogger<AuthController> logger)
        {
            _accountService = accountService;
            _repo = repo;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var result = _accountService.Authenticate(login);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult SignIn([FromBody] SignInModel newUser)
        {
            string hashedPass = EncryptionHelper.CreateHashed(newUser.Password);

            var addedUser = new AppUser
            {
                Email = newUser.Email,
                HashedPassword = hashedPass,
                Role = newUser.Role
            };

            _repo.Add(addedUser);

            return Ok(_repo.GetByFilter(u => u.Email == newUser.Email));
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult GetAllUsers()
            => Ok(_repo.GetListByFilter(null));

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            string currentUserToken = await HttpContext.GetTokenAsync("access_token");

            var currentUser = _repo.GetByFilter(u => u.Token == currentUserToken);

            return Ok(currentUser);
        }
    }
}