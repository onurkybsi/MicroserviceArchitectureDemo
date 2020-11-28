using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Infrastructure
{
    public abstract class ModuleDescriptor<T> : IModuleDescriptor
    {
        protected static T instance;

        protected ModuleDescriptor() { }

        public static T GetDescriptor()
            => instance ?? (instance = (T)Activator.CreateInstance(typeof(T)));

        public abstract List<ServiceDescriptor> GetDescriptions();

        public IServiceCollection Describe(IServiceCollection services)
        {
            GetDescriptions()?.ForEach(d => services.Add(d));

            return services;
        }
    }
}