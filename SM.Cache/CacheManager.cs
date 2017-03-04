using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SM.Cache
{
    public class CacheManager
    {
        const string CACHE_DB_SUFFIX = "_Table";
        const string CACHE_PROP_SUFFIX = "_Properties";
        private static CacheManager _Instance;
        private MemoryCache Cache;
        private CacheManager()
        {
            Cache = MemoryCache.Default;
        }
        public static CacheManager Instance
        {
            get
            {
                if (_Instance == null) { _Instance = new CacheManager(); }
                return _Instance;
            }
        }
        public void SetCacheDbTable(Type objectType, string tableName)
        {
            var typeName = objectType.FullName + CACHE_DB_SUFFIX;
            var value = Cache.Get(typeName);
            if (value == null)
            {
                value = tableName;
                Cache.Set(typeName, value, null);
            }
        }
        public void SetCacheProperties(Type objectType)
        {
            var typeName = objectType.FullName + CACHE_PROP_SUFFIX;
            var value = Cache.Get(typeName);
            if (value == null)
            {
                value = objectType.GetProperties();
                Cache.Set(typeName, value, null);
            }
        }
        public string GetCachedDbTable(Type objectType)
        {
            return GetCachedValue<string>(objectType.FullName + CACHE_DB_SUFFIX);
        }
        public string GetCachedProperties(Type objectType)
        {
            return GetCachedValue<string>(objectType.FullName + CACHE_PROP_SUFFIX);
        }

        public T GetCachedValue<T>(string key)
        {
            var value = Cache.Get(key);
            return value == null ? default(T) : (T)value;
        }
    }
}
