using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;
using System.Threading;

namespace CN.UppgiftBE.Web.Util
{
    public interface ICacheManagerService
    {
        T Get<T>(string id) where T : class, new();
        bool Add<T>(string id, T data, DateTime expires) where T : class, new();
        T Delete<T>(string id) where T : class, new();
    }
    public class CacheManagerService : ICacheManagerService
    {
        public T Get<T>(string id) where T : class, new()
        {
            return MemoryCache.Default.Get(id) as T;
            
        }
            
        public bool Add<T>(string id, T data, DateTime expires) where T : class, new()
        {
            return MemoryCache.Default.Add(id, data, new CacheItemPolicy()
            {
                AbsoluteExpiration = new DateTimeOffset(expires)
            });
        }

        public T Delete<T>(string id) where T : class, new()
        {
            return MemoryCache.Default.Remove(id) as T;
        }
    }
}