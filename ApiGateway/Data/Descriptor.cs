using System.Collections.Generic;
using ApiGateway.Data.AppUser;
using ApiGateway.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Data
{
    public class Descriptor : IModuleDescriptor
    {
        public List<ServiceDescriptor> Describe()
        {
            List<ServiceDescriptor> descriptions = new List<ServiceDescriptor>();

            descriptions.Add(ServiceDescriptor.Singleton(typeof(IAppUserRepo), typeof(AppUserRepo)));

            return descriptions;
        }
    }
}