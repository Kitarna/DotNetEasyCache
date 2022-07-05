using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EasyCache.Cache.Interface;
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

            services.AddScoped<IEasyCache, EasyCache>();

            string cacheType = configuration.GetSection("EasyCache:CacheHandler").Value;

            if (cacheType == Constants.REDIS)
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = configuration.GetSection("EasyCache:Redis:Host").Value;
                    options.InstanceName = configuration.GetSection("EasyCache:Redis:Instance").Value;
                });
            }
        }
    }
}
