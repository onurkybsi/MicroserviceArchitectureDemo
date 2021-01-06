using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Utility
{
    /// <summary>
    /// Generic abstract IModuleDescriptor base implementation
    /// </summary>
    public abstract class ModuleDescriptor<T> : IModuleDescriptor
    {
        protected static T instance;

        protected ModuleDescriptor() { }

        /// <summary>
        /// Creates the described module descriptor and returns.
        /// </summary>
        /// <returns>
        /// Returns the described module descriptor
        /// </returns>
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