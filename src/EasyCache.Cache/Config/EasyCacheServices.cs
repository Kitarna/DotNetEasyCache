using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Alachisoft.NCache.Caching.Distributed;
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

            switch (cacheType)
            {
                case Constants.REDIS:
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = configuration.GetSection("EasyCache:Redis:Host").Value;
                        options.InstanceName = configuration.GetSection("EasyCache:Redis:Instance").Value;
                    });
                    break;
                case Constants.MEMORY:
                    services.AddDistributedMemoryCache();
                    break;
                case Constants.SQL:
                    services.AddDistributedSqlServerCache(options =>
                    {
                        options.ConnectionString = configuration.GetSection("EasyCache:Sql:CacheConnectionString").Value;
                        options.SchemaName = configuration.GetSection("EasyCache:Sql:SchemaName").Value;
                        options.TableName = configuration.GetSection("EasyCache:Sql:TableName").Value;
                    });
                    break;
                case Constants.NCACHE:
                    services.AddNCacheDistributedCache(options =>
                    {
                        options.CacheName = configuration.GetSection("EasyCache:NCache:CacheName").Value;
                        options.EnableLogs = bool.Parse(configuration.GetSection("EasyCache:NCache:EnableLogs").Value);
                        options.ExceptionsEnabled = bool.Parse(configuration.GetSection("EasyCache:NCache:ExceptionsEnabled").Value);
                    });
                    break;
                default:
                    break;
            }   
        }
    }
}
