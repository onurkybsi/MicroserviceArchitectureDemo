using KybInfrastructure.Model;

namespace Infrastructure.Framework.Grpc
{
    public class ServiceCallResult<TResponse> : ProcessResult
    {
        public TResponse ServiceResponse { get; set; }
    }
}