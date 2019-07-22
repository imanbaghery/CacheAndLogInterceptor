using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheInterceptor
{
    public class CacheMethodAttribute:Attribute
    {
        public CacheMethodAttribute()
        {
            SecondsToCache = 10;
        }

        public int SecondsToCache { get; set; }
    }
}
