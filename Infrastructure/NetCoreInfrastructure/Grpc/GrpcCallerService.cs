using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Grpc
{
    public static class GrpcCallerService
    {
        private static string RPC_EXCEPTION_MESSAGE = "Error occurred when service call";
        private static string GENERAL_EXCEPTION_MESSAGE = "Unknown error occurred";

        public static async Task<ServiceCallResult<TResponse>> CallService<TResponse>(ILogger logger, Func<Task<TResponse>> call)
        {
            var serviceCallResult = new ServiceCallResult<TResponse>();

            try
            {
                var responseFromService = await call();

                serviceCallResult.IsSuccess = true;
                serviceCallResult.ServiceResponse = responseFromService;

                return serviceCallResult;
            }
            catch (RpcException rex)
            {
                logger?.LogError("Error calling via grpc: {Status} - {Message}", rex.Status, rex.Message);

                serviceCallResult.Message = RPC_EXCEPTION_MESSAGE;

                return serviceCallResult;
            }
            catch (Exception ex)
            {
                logger?.LogError("Error occured: {Message}", ex.Message);

                serviceCallResult.Message = GENERAL_EXCEPTION_MESSAGE;

                return serviceCallResult;
            }
        }

        public static async Task<ServiceCallResult<TResponse>> CallService<TResponse>(Func<Task<TResponse>> call)
            => await CallService(null, call);
    }
}
