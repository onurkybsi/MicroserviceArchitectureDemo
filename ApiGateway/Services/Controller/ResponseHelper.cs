using System;
using Infrastructure.Grpc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Services.Controller
{
    public static class ResponseHelper
    {
        public static IActionResult PrepareActionResponse<T>(ControllerBase controllerBase, ServiceCallResult<T> response)
        {
            if (response.IsSuccess)
                return controllerBase.Ok(response.ServiceResponse);
            else
            {
                return controllerBase.StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    response.IsSuccess,
                    response.Message
                });
            }
        }

        public static IActionResult PrepareActionResponse<T, TResponseContent>(ControllerBase controllerBase, ServiceCallResult<T> response, Func<ServiceCallResult<T>, object> succesResponse)
        {
            if (response.IsSuccess)
                return controllerBase.Ok(succesResponse.Invoke(response));
            else
            {
                return controllerBase.StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    response.IsSuccess,
                    response.Message
                });
            }
        }
    }
}