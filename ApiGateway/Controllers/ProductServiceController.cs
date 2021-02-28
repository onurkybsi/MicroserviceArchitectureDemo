using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ApiGateway.Services.Controller;
using Infrastructure.Framework.Grpc;

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
            var response = await GrpcCallerService.CallService<Service.GetByIdResponse>(async () => await _client.GetByIdAsync(request, deadline: DateTime.UtcNow.AddSeconds(30)));

            if (!response.IsSuccessful)
                _logger.LogError($"{nameof(ProductServiceController)} call unsuccessful on GetProductById: {response.Message}");

            return ResponseHelper.PrepareServiceCallResponse(this, response.ServiceResponse.ServiceProcessResult.IsSuccessful, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetListByQuery(Service.GetListByQueryRequest request)
        {
            var response = await GrpcCallerService.CallService<Service.GetListByQueryResponse>(async ()
                => await _client.GetListByQueryAsync(request, deadline: DateTime.UtcNow.AddSeconds(30)));

            if (!response.IsSuccessful)
                _logger.LogError($"{nameof(ProductServiceController)} call unsuccessful on GetListByQuery: {response.Message}");

            return ResponseHelper.PrepareServiceCallResponse(this, response.ServiceResponse.ServiceProcessResult.IsSuccessful, response);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Service.Product request)
        {
            var response = await GrpcCallerService.CallService<Service.SaveResponse>(async () => await _client.SaveAsync(request, deadline: DateTime.UtcNow.AddSeconds(30)));

            if (!response.IsSuccessful)
                _logger.LogError($"{nameof(ProductServiceController)} call unsuccessful on Save: {response.Message}");


            return ResponseHelper.PrepareServiceCallResponse(this, response.ServiceResponse.ServiceProcessResult.IsSuccessful, response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Service.DeleteByIdRequest request)
        {
            var response = await GrpcCallerService.CallService<Service.DeleteByIdResponse>(async () => await _client.DeleteAsync(request, deadline: DateTime.UtcNow.AddSeconds(30)));

            if (!response.IsSuccessful)
                _logger.LogError($"{nameof(ProductServiceController)} call unsuccessful on Save: {response.Message}");


            return ResponseHelper.PrepareServiceCallResponse(this, response.ServiceResponse.ServiceProcessResult.IsSuccessful, response);
        }
    }
}