using System.Collections.Generic;
using ApiGateway.Data.AppUser;
using ApiGateway.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Data
{
    public class Descriptor : IModuleDescriptor
    {
        private static Descriptor instance;

        private static List<ServiceDescriptor> Descriptions = new List<ServiceDescriptor>()
        {
            ServiceDescriptor.Singleton(typeof(IAppUserRepo), typeof(AppUserRepo))
        };

        private Descriptor() { }

        public static Descriptor GetDescriptor()
            => instance ?? (instance = new Descriptor());

        public List<ServiceDescriptor> GetDescriptions()
            => Descriptions;

        public IServiceCollection Describe(IServiceCollection services)
        {
            Descriptions.ForEach(d => services.Add(d));

            return services;
        }
    }
}