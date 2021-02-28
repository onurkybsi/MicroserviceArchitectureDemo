namespace KybInfrastructure.Framework.Grpc
{
    public interface IGrpcClientPool<TClient>
    {
        TClient Get();
        void Return(TClient client);
    }
}