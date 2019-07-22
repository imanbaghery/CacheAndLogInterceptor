using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLoggerAspect
{
    class Program
    {
        static void Main(string[] args)
        {
            var myType = SmObjectFactory.Container.GetInstance<IMyType>();
            Console.WriteLine(myType.DoSomething(10, "Test", new MyArg { Id = 1, Name = "Mammad" }));
        }
    }
}
