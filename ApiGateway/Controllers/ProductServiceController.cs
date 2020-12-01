using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static ApiGateway.Services.GrpcService.Helper;

namespace ApiGateway.Controllers.ProductService
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductServiceController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Channel _channel;

        public ProductServiceController(IConfiguration configuration, ChannelResolver channelResolver)
        {
            _configuration = configuration;
            _channel = channelResolver(_configuration["PRODUCT_SERVICE_URL"]);
        }

        [HttpGet]
        public IActionResult GetList(string message)
        {
            var client = new Product.ProductService.ProductServiceClient(_channel);

            var reply = client.GetList(new Product.GetListRequest { Query = string.Empty });

            return Ok(reply.Products);
        }
    }
}