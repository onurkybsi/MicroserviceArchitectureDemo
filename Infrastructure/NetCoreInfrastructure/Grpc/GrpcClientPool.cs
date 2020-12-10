using System;
using System.Collections.Concurrent;
using Grpc.Core;

namespace Infrastructure.Grpc
{
    public class GrpcClientPool<TClient>
    {
        private readonly ConcurrentBag<TClient> _clients;
        private readonly Channel _channel;

        public GrpcClientPool(GrpcClientPoolConfig poolConfig)
        {
            CheckPoolConfigAvailability(poolConfig);

            _channel = new Channel(poolConfig.TargetServerURL, ChannelCredentials.Insecure);

            _clients = new ConcurrentBag<TClient>();
        }

        public TClient Get() => _clients.TryTake(out TClient client) ? client : (TClient)Activator.CreateInstance(typeof(TClient), _channel);

        public void Return(TClient client) => _clients.Add(client);

        private void CheckPoolConfigAvailability(GrpcClientPoolConfig poolConfig)
        {
            if (poolConfig is null)
                throw new ArgumentNullException(nameof(poolConfig));
            else if (poolConfig.TargetServerURL is null)
                throw new ArgumentNullException(nameof(poolConfig.TargetServerURL));
            else if (poolConfig.TargetServerURL.Trim() == "")
                throw new ArgumentException(nameof(poolConfig.TargetServerURL));
        }
    }
}