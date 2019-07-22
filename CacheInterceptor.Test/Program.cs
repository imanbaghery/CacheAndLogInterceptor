using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Reflection.Emit;
using TestLoggerAspect;

namespace CacheInterceptor.Test
{
    class Program
    {
        static void Main(string[] args)
        {
	        var myType = SmObjectFactory.Container.GetInstance<IMyType>();

			for (int i = 0; i < 100; i++)
	        {
				var numbers = myType.GetRandomNumbers(10);
		        foreach (var number in numbers)
		        {
			        Console.Write(number+",");
		        }
				Console.WriteLine("-----");
		        Thread.Sleep(3000);
			}
	        Console.ReadLine();

        }

    }
}
