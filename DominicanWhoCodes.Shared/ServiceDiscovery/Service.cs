
using Microsoft.Extensions.Configuration;
using System;

namespace DominicanWhoCodes.Shared.ServiceDiscovery
{
    public class Service
    {
        public string ServiceId { get; private set; }
        public Uri ServiceDiscoveryAddress { get; private set; }
        public Uri ServiceAddress { get; private set; }
        public string ServiceName { get; private set; }

        public static Service GetService(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var serviceConfig = new Service
            {
                ServiceDiscoveryAddress = new Uri(configuration["ServiceConfig:ServiceDiscoveryAddress"]),
                ServiceAddress = new Uri(configuration["ServiceConfig:ServiceAddress"]),
                ServiceName = configuration["ServiceConfig:ServiceName"],
                ServiceId = configuration["ServiceConfig:ServiceId"]
            };

            return serviceConfig;
        }
    }
}
