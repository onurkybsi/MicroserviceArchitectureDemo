using System;
using Grpc.Core;

namespace Infrastructure.Grpc
{
    public class GrpcClientPoolConfig
    {
        public string TargetServerURL { get; set; }
    }
}