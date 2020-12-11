using System.Collections.Generic;
using ApiGateway.Data.AppUser;
using Infrastructure.Utility;
using Microsoft.Extensions.DependencyInjection;

namespace ApiGateway.Data
{
    public class Descriptor : ModuleDescriptor<Data.Descriptor>
    {
        private static List<ServiceDescriptor> Descriptions = new List<ServiceDescriptor>()
        {
            ServiceDescriptor.Singleton(typeof(IAppUserRepo), typeof(AppUserRepo))
        };

        public override List<ServiceDescriptor> GetDescriptions()
            => Descriptions;
    }
}