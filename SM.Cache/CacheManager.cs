using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            SetCacheValue(typeName, tableName);
        }
        public string GetCachedDbTable(Type objectType)
        {
            return GetCachedValue<string>(objectType.FullName + CACHE_DB_SUFFIX);
        }
        public PropertyInfo[] GetCachedProperties(Type objectType)
        {
            var props = GetCachedValue<PropertyInfo[]>(objectType.FullName + CACHE_PROP_SUFFIX);
            if (props == null)
            {
                var key = objectType.FullName + CACHE_PROP_SUFFIX;
                props = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                SetCacheValue(key, props);
            }
            return props;
        }

        public T GetCachedValue<T>(string key)
        {
            var value = Cache.Get(key);
            return value == null ? default(T) : (T)value;
        }
        public void SetCacheValue(string key, object value)
        {
            var v = Cache.Get(key);
            if (v == null)
            {
                Cache.Set(key, value, null);
            }
        }
    }
}
