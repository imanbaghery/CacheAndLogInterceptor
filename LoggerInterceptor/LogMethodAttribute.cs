using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerInterceptor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LogMethodAttribute:Attribute
    {
        public LogMethodAttribute()
        {
            LogInput = true;
            LogOutput = true;
            LogException = true;
        }

        public bool LogInput { get; set; }
        public bool LogOutput { get; set; }
        public bool LogException { get; set; }
    }
}
