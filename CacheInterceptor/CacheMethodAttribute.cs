using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheInterceptor
{
    public class CacheMethodAttribute:Attribute
    {
        public CacheMethodAttribute(int second)
        {
            SecondsToCache = second;
        }

        public int SecondsToCache { get; set; }
    }
}
