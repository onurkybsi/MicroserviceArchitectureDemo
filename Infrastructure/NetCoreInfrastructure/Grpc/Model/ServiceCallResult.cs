using Infrastructure.Models;

namespace Infrastructure.Grpc
{
    public class ServiceCallResult<TResponse> : ProcessResult
    {
        public TResponse ServiceResponse { get; set; }
    }
}