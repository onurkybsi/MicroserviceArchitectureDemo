using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiGateway.Controllers.ProductService
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductServiceController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductServiceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetList(string message)
        {
            Channel channel = new Channel(_configuration["PRODUCT_SERVICE_URL"], ChannelCredentials.Insecure);

            var client = new Product.ProductService.ProductServiceClient(channel);

            var reply = client.GetList(new Product.GetListRequest { Query = string.Empty });

            channel.ShutdownAsync().Wait();

            return Ok(reply.Products);
        }
    }
}