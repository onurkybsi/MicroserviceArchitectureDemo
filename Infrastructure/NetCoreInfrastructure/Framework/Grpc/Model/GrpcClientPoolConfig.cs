using System;
using Grpc.Core;

namespace Infrastructure.Framework.Grpc
{
    public class GrpcClientPoolConfig
    {
        public string TargetServerURL { get; set; }
    }
}