using System;
using System.Threading.Tasks;
using Infrastructure.Grpc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiGateway.Services.Controller;

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
            var response = await GrpcCallerService.CallService<Service.GetByIdResponse>(_logger,
                async () => await _client.GetByIdAsync(request, deadline: DateTime.UtcNow.AddSeconds(30)));

            return ResponseHelper.PrepareActionResponse(this, response);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Service.Product product)
        {
            var response = await GrpcCallerService.CallService<Service.SaveResponse>(_logger,
                async () => await _client.SaveAsync(product, deadline: DateTime.UtcNow.AddSeconds(30)));

            return ResponseHelper.PrepareActionResponse(this, response);
        }
    }
}