namespace ApiGateway.Services.GrpcService
{
    public static class Helper
    {
        public delegate Grpc.Core.Channel ChannelResolver(string target);
    }
}