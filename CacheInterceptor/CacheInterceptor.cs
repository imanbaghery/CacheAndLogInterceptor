using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Castle.DynamicProxy;

namespace CacheInterceptor
{
    public class CacheInterceptor:IInterceptor
    {
        private static object _lockObject=new object();
        public void Intercept(IInvocation invocation)
        {
            CacheMethod(invocation);
	        

		}

        private void CacheMethod(IInvocation invocation)
        {
            var cacheMethodAttribute = getCacheMethodAttribute(invocation);
            if (cacheMethodAttribute==null)
            {
                invocation.Proceed();
                return;
            }

            var cacheDuration = ((CacheMethodAttribute) cacheMethodAttribute).SecondsToCache;

            var cacheKey = getCacheKey(invocation);

            var cache = HttpRuntime.Cache;

            var cachedResult = cache.Get(cacheKey);

            if (cachedResult!=null)
            {
                invocation.ReturnValue = cachedResult;
            }
            else
            {
                lock (_lockObject)
                {
					// در غير اينصورت ابتدا متد را اجرا كرده
					invocation.Proceed();
					if (invocation.ReturnValue == null)
						return;

					// سپس نتيجه آن‌را كش مي‌كنيم
					cache.Insert(key: cacheKey,
								 value: invocation.ReturnValue,
								 dependencies: null,
								 absoluteExpiration: DateTime.Now.AddSeconds(cacheDuration),
								 slidingExpiration: TimeSpan.Zero);
				}
            }

        }

        private string getCacheKey(IInvocation invocation)
        {
            var cacheKey = invocation.Method.Name;
            foreach (var invocationArgument in invocation.Arguments)
            {
                cacheKey += $": {invocationArgument}";
            }
            return cacheKey;
        }

        private Attribute getCacheMethodAttribute(IInvocation invocation)
        {
            var methodInfo = invocation.MethodInvocationTarget ?? invocation.Method;
            var cacheMethodAttribute= methodInfo.GetCustomAttribute(typeof(CacheMethodAttribute));
            return cacheMethodAttribute;
        }
    }

}
