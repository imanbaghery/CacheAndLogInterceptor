using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLoggerAspect;

namespace CacheInterceptor.Test
{
	public class MyType:IMyType
	{
		[CacheMethod(5)]
		public List<int> GetRandomNumbers(int count)
		{
			List<int> numbers = new List<int>();
			Random random = new Random();

			for (int i = 0; i < count; i++)
			{
				numbers.Add(random.Next(0, 1000));
			}
			return numbers;
		}
	}
}
