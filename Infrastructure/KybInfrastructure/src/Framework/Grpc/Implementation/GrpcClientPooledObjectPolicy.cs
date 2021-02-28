using System;
using Grpc.Core;
using Microsoft.Extensions.ObjectPool;

namespace KybInfrastructure.Framework.Grpc
{
    public class GrpcClientPooledObjectPolicy<TClient> : PooledObjectPolicy<TClient>
    {
        private Channel _channel;

        public GrpcClientPooledObjectPolicy(Channel channel)
        {
            _channel = channel;
        }

        public override TClient Create()
            => (TClient)Activator.CreateInstance(typeof(TClient), _channel);


        public override bool Return(TClient obj)
            => true;
    }
}