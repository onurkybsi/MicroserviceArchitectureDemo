using System;
using System.Collections.Concurrent;
using Grpc.Core;

namespace KybInfrastructure.Framework.Grpc
{
    public class GrpcClientPool<TClient> : IGrpcClientPool<TClient>
    {
        private readonly ConcurrentBag<TClient> _clients;
        private readonly Channel _channel;

        public GrpcClientPool(GrpcClientPoolSettings poolSettings)
        {
            CheckPoolSettpoolSettingsAvailability(poolSettings);

            _channel = new Channel(poolSettings.TargetServerURL, ChannelCredentials.Insecure);

            _clients = new ConcurrentBag<TClient>();
        }

        public TClient Get() => _clients.TryTake(out TClient client) ? client : (TClient)Activator.CreateInstance(typeof(TClient), _channel);

        public void Return(TClient client) => _clients.Add(client);

        private void CheckPoolSettpoolSettingsAvailability(GrpcClientPoolSettings poolSettings)
        {
            if (poolSettings is null)
                throw new ArgumentNullException(nameof(poolSettings));
            else if (poolSettings.TargetServerURL is null)
                throw new ArgumentNullException(nameof(poolSettings.TargetServerURL));
            else if (poolSettings.TargetServerURL.Trim() == "")
                throw new ArgumentException(nameof(poolSettings.TargetServerURL));
        }
    }
}