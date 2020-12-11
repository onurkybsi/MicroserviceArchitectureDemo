using Infrastructure.Framework.Grpc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Services.Controller
{
    public static class ResponseHelper
    {
        public static IActionResult PrepareServiceCallResponse<T>(ControllerBase controllerBase, bool isServiceProcessSuccess, ServiceCallResult<T> response)
        {
            if (isServiceProcessSuccess)
                return controllerBase.Ok(response.ServiceResponse);
            else
            {
                return controllerBase.StatusCode(StatusCodes.Status500InternalServerError, response.ServiceResponse);
            }
        }
    }
}