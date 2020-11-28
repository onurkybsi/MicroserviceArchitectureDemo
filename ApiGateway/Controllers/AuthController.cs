using System;
using System.Linq;
using ApiGateway.Data.AppUser;
using ApiGateway.Model;
using ApiGateway.Services.Auth;
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var result = _accountService.Authenticate(login);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest();
        }

        [Authorize(Roles = "Admin")]
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

            return Ok(_repo.Get(u => u.Email == newUser.Email));
        }

        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            int currentUserId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(cu => cu.Type == "userId")?.Value);

            AppUser currentUser = _repo.Get(cu => cu.Id == currentUserId);

            return Ok(currentUser);
        }
    }
}