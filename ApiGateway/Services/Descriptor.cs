using System.Collections.Generic;
using ApiGateway.Infrastructure;
using ApiGateway.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Services
{
    public class Descriptor : IModuleDescriptor
    {
        public List<ServiceDescriptor> Describe()
        {
            List<ServiceDescriptor> descriptions = new List<ServiceDescriptor>();

            descriptions.Add(ServiceDescriptor.Singleton(typeof(IAuthService), typeof(AuthService)));

            return descriptions;
        }
    }
}