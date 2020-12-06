using System;
using Infrastructure.Grpc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Services.Controller
{
    public static class ResponseHelper
    {
        public static IActionResult PrepareServiceCallResponse<T>(ControllerBase controllerBase, ServiceCallResult<T> response)
        {
            if (response.IsSuccess)
                return controllerBase.Ok(response.ServiceResponse);
            else
            {
                return controllerBase.StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    response.IsSuccess,
                    // TO-DO: Servisten hata kullanıcıya dönmemeli. 
                    // Buraya hard coded yazdım şimdilik ama servisten handle ettiğimiz hataları kullanıcıya nasıl döneceğimize düşünelim.
                    Message = "Error occured !"
                });
            }
        }

        public static IActionResult PrepareServiceCallResponse<T, TResponseContent>(ControllerBase controllerBase, ServiceCallResult<T> response, Func<ServiceCallResult<T>, object> succesResponse)
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