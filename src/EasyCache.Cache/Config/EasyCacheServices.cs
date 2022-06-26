using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyCache.Cache.Config
{
    public static class EasyCacheServices
    {
        public static void SetEasyCacheServices(this IServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Directory where the json files are located
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddScoped<EasyCache>();

            string cacheType = configuration.GetSection("CacheHandler").Value;

            if (cacheType == Constants.REDIS)
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration.GetSection("Redis:Host").Value;
                    options.InstanceName = configuration.GetSection("Redis:Instance").Value;
                });
            }
        }
    }
}
