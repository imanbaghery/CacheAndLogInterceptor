using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerInterceptor;

namespace TestLoggerAspect
{
    public class MyType:IMyType
    {
        [LogMethod(LogInput = true,LogOutput = false)]
        public MyReturnType DoSomething(int number, string data, MyArg myArg)
        {
            return new MyReturnType { Amount = 100, MyArg = myArg };
        }
    }
}
