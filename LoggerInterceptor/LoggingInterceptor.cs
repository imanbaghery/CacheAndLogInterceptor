using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using NLog;
using NLog.Fluent;

namespace LoggerInterceptor
{
    public class LoggingInterceptor : IInterceptor
    {
        // private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        //inject with structuremap
        private readonly ILogger _logger;
        private static object _lockObject = new object();

        public LoggingInterceptor(ILogger logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            LoggMethod(invocation);
        }

        private void LoggMethod(IInvocation invocation)
        {
            var logMethodAttribute = getLogMethodAttribute(invocation);
            if (logMethodAttribute == null)
            {
                invocation.Proceed();
                return;
            }

            string methodName = $" 'methodName': {invocation.Method.DeclaringType?.FullName} {invocation.Method.Name}(), ";
            try
            {


                    //لاگ پارامترهای ورودی
                    var logInput = ((LogMethodAttribute) logMethodAttribute).LogInput;
                    if (logInput)
                    {
                        string args = string.Empty;
                        for (int i = 0; i < invocation.Arguments.Length; i++)
                        {
                            args += $"'arg{i}': '{invocation.Arguments[i].ToString()}', ";
                        }
                        _logger.Info($"Start Method '{methodName}' {args}");
                    }

                    invocation.Proceed(); //فراخواني متد اصلي


                    //لاگ مقدار خروجی
                    var logOutput = ((LogMethodAttribute) logMethodAttribute).LogOutput;
                    if (logOutput)
                    {
                        var returnProperties = invocation.ReturnValue.GetType().GetProperties();
                        string returnValues = string.Empty;
                        foreach (var returnProperty in returnProperties)
                        {
                            var name = returnProperty.Name;
                            var value = returnProperty.GetValue(invocation.ReturnValue, null);
                            returnValues += $"'{name}': '{value}, '";
                        }
                        _logger.Info($"Success Method '{methodName}' {returnValues}");
                    }
            }
            catch (Exception ex)
            {
               
                    var logException=((LogMethodAttribute) logMethodAttribute).LogException;
                    if (logException)
                    {
                            _logger.Error(ex, $"Error at Method {methodName}");
                    }

                throw;
            }
            finally
            {
                Console.WriteLine($"Exit from Method {methodName}");
            }
        }

        private Attribute getLogMethodAttribute(IInvocation invocation)
        {
            var methodInfo = invocation.MethodInvocationTarget ?? invocation.Method;
            var logMethodAttribute = methodInfo.GetCustomAttribute(typeof(LogMethodAttribute));
           // var logMethodAttribute2 = Attribute.GetCustomAttribute(methodInfo, typeof(LogMethodAttribute));
            return logMethodAttribute;
        }
    }
}
