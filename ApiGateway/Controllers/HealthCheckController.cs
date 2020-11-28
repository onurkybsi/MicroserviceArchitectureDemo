using System.Linq;
using System.Security.Claims;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ApiGateway.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;

        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Check()
        {
            _logger.LogInformation($"{HttpContext.User.Claims.First(c => c.Type == "userId")} in api/test/test");

            CheckServiceConnection($"Hello from ApiGateway ! It is {HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value}");

            return StatusCode(200);
        }

        private void CheckServiceConnection(string message)
        {
            Channel channel = new Channel("127.0.0.1:30051", ChannelCredentials.Insecure);

            var client = new Connectioncheck.Greet.GreetClient(channel);

            var reply = client.SayHello(new Connectioncheck.HelloRequest { Name = message });
            Log.Information("Greeting: " + reply.Message);

            channel.ShutdownAsync().Wait();
        }
    }
}