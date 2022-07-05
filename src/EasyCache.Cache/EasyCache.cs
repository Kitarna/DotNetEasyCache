using EasyCache.Cache.Interface;
using Microsoft.Extensions.Caching.Distributed;

namespace EasyCache.Cache
{
    public class EasyCache : IEasyCache
    {
        private readonly IDistributedCache _cache;

        public EasyCache(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Adds the value sent to the Redis cache for the amount of seconds given.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        public void Put(string key, string value, int seconds = 0)
        {
            DistributedCacheEntryOptions? options = null;

            options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(seconds));

            _cache.SetString(key, value, options);
        }

        /// <summary>
        /// Adds the value sent to the cache asynchronously for the amount of seconds given.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds"></param>
        public async void PutAsync(string key, string value, int seconds = 0)
        {
            DistributedCacheEntryOptions? options = null;

            options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(seconds));

            await _cache.SetStringAsync(key, value, options);
        }

        /// <summary>
        /// Adds the value sent to the Redis cache indefinitely. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Forever(string key, string value)
        {
            _cache.SetString(key, value);
        }

        /// <summary>
        /// Adds the value sent to the cache asynchronously indefinitely. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public async void ForeverAsync(string key, string value)
        {
            await _cache.SetStringAsync(key, value);
        }

        /// <summary>
        /// Retrieves the value of the key supplied
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(string key)
        {
            var existingItem = _cache.GetString(key);

            return string.IsNullOrEmpty(existingItem) ? "" : existingItem.ToString();
        }
        /// <summary>
        /// Retrieves the value of the key supplied asynchronously
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key)
        {
            var existingItem = await _cache.GetStringAsync(key);

            return string.IsNullOrEmpty(existingItem) ? "" : existingItem.ToString();
        }
        /// <summary>
        /// Deletes the cached value based on the key supplied.
        /// </summary>
        /// <param name="key"></param>
        public void Forget(string key)
        {
            _cache.Remove(key);
        }
        /// <summary>
        /// Deletes the cached value based on the key supplied asynchronously
        /// </summary>
        /// <param name="key"></param>
        public async void ForgetAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
        /// <summary>
        /// Validates if the key exists in the cache.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            var item = _cache.GetString(key);

            return !string.IsNullOrEmpty(item);
        }
        /// <summary>
        /// Validates if the key exists in the cache asynchronously.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key)
        {
            var item = await _cache.GetStringAsync(key);

            return !string.IsNullOrEmpty(item);
        }
    }
}