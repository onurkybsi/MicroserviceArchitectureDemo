using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Utility
{
    // ModuleDescriptor ler IDisposable olabilir tanımlar register olduktan sonra bu instance'larını yaşamasına gerek yok.
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