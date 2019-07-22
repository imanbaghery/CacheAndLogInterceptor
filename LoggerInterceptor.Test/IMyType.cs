using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoggerAspect
{
    public interface IMyType
    {
        MyReturnType DoSomething(int number, string data, MyArg myArg);
    }
}
