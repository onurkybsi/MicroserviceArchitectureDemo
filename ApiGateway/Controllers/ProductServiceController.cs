using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers.ProductService
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductServiceController : ControllerBase
    {
        private readonly Service.ProductService.ProductServiceClient _client;

        public ProductServiceController(Service.ProductService.ProductServiceClient client)
        {
            _client = client;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetList(string message)
        {
            var reply = _client.GetList(new Service.GetListRequest { Query = string.Empty });

            return await Task.FromResult(Ok(reply.Products));
        }
    }
}