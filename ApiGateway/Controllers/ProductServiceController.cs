using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiGateway.Controllers.ProductService
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductServiceController : ControllerBase
    {
        private readonly Service.ProductService.ProductServiceClient _client;
        private readonly ILogger<ProductServiceController> _logger;

        public ProductServiceController(Service.ProductService.ProductServiceClient client, ILogger<ProductServiceController> logger)
        {
            _client = client;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Service.GetByIdRequest request)
        {
            var reply = await _client.GetByIdAsync(request);

            return Ok(reply.Product);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Service.Product product)
        {
            var reply = await _client.SaveAsync(product);

            return Ok(reply);
        }
    }
}