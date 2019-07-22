using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using CacheInterceptor;

using StructureMap;
using CacheInterceptor = CacheInterceptor.CacheInterceptor;

namespace TestLoggerAspect
{
    public static class SmObjectFactory
    {
        private static readonly Lazy<Container> _containerBuilder =
            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return _containerBuilder.Value; }
        }

        private static Container defaultContainer()
        {
            return new Container(ioc =>
            {
                var dynamicProxy = new ProxyGenerator();
                ioc.Scan(scanner =>
                {
                    scanner.AssemblyContainingType<IMyType>(); // نحوه يافتن اسمبلي لايه سرويس

                    // Connect `IName` interface to 'Name' class automatically
                    scanner.WithDefaultConventions();
                });

                //ioc.For<IMyType>().DecorateAllWith(myType => dynamicProxy.CreateInterfaceProxyWithTarget(myType,new LoggingInterceptor()));
                //ioc.For<ILogger>().Singleton().Use(a => LogManager.GetCurrentClassLogger());
                ioc.For<IMyType>().DecorateAllWith(myType => dynamicProxy.CreateInterfaceProxyWithTarget(myType, Container.GetInstance<global::CacheInterceptor.CacheInterceptor>()));

            });
        }
    }
}
