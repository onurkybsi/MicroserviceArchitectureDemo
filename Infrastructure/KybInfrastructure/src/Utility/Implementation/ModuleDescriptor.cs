using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace KybInfrastructure.Utility
{
    /// <summary>
    /// Generic abstract IModuleDescriptor base implementation
    /// </summary>
    public abstract class ModuleDescriptor<TModule, TModuleContext> : IModuleDescriptor
    {
        protected static TModule instance;

        protected ModuleDescriptor() { }

        /// <summary>
        /// Creates the described module descriptor and returns.
        /// </summary>
        /// <returns>
        /// Returns the described module descriptor
        /// </returns>
        public static TModule GetDescriptor(TModuleContext moduleContext)
            => instance ?? (instance = (TModule)Activator.CreateInstance(typeof(TModule), moduleContext));

        public abstract List<ServiceDescriptor> GetDescriptions();

        public IServiceCollection Describe(IServiceCollection services)
        {
            GetDescriptions()?.ForEach(d => services.Add(d));

            return services;
        }
    }
}