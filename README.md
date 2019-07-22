CacheInterceptor Can Cache method with same input param in "System.Web.Caching.Cache",
for example:

[CacheMethod(10)]
public object myMethod()
{
//anything
}

===============================================================================


LoggerInterceptor can log input and output methos param with Attribute,
for example:

[LogMethod(LogInput = true,LogOutput = false)]
public void myMethod()
{
//anything
}
