using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Utility
{
    /// <summary>
    /// Provides a module to be defined to another module via IServiceCollection container
    /// </summary>
    public interface IModuleDescriptor
    {
        List<ServiceDescriptor> GetDescriptions();
        IServiceCollection Describe(IServiceCollection services);
    }
}