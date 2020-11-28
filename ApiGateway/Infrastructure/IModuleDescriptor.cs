using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Infrastructure
{
    public interface IModuleDescriptor
    {
        List<ServiceDescriptor> GetDescriptions();
        IServiceCollection Describe(IServiceCollection services);
    }
}