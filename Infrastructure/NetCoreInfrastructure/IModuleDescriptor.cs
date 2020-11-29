using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public interface IModuleDescriptor
    {
        List<ServiceDescriptor> GetDescriptions();
        IServiceCollection Describe(IServiceCollection services);
    }
}