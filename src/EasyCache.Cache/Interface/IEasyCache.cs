namespace EasyCache.Cache.Interface
{
    public interface IEasyCache
    {
        void Put(string key, string value, int seconds = 0);
        void PutAsync(string key, string value, int seconds = 0);
        void Forever(string key, string value);
        public void ForeverAsync(string key, string value);
        public string Get(string key);
        public Task<string> GetAsync(string key);
        public void Forget(string key);
        public void ForgetAsync(string key);
        public bool Exists(string key);
        public Task<bool> ExistsAsync(string key);
    }
}