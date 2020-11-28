using System.Collections.Generic;
using ApiGateway.Infrastructure;
using ApiGateway.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Services
{
    public class Descriptor : ModuleDescriptor<Services.Descriptor>
    {
        private static List<ServiceDescriptor> Descriptions = new List<ServiceDescriptor>()
        {
            ServiceDescriptor.Singleton(typeof(IAuthService), typeof(AuthService))
        };

        public override List<ServiceDescriptor> GetDescriptions()
            => Descriptions;
    }
}