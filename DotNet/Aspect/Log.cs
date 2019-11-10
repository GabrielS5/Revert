using AspectInjector.Broker;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace AspectCore
{
    [Aspect(Scope.Global)]
    [Injection(typeof(Log))]
    public class Log : Attribute
    {
        private readonly ILogger logger;
        public Log()
        {
            logger = new LoggerFactory().AddFile("Logs/aspectLog").CreateLogger(typeof(Log));
        }


        [Advice(Kind.Around, Targets = Target.Method)]
        public object HandleMethod(
            [Argument(Source.Name)] string name,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Target)] Func<object[], object> method)
        {
            Console.WriteLine($"Executing method {name}");

            var sw = Stopwatch.StartNew();

            var result = method(arguments);

            sw.Stop();

            var argumentsString = "";

            foreach(object arg in arguments){
                argumentsString = argumentsString + "  " + arg.ToString();
            }

            var da = result.GetType();
            logger.LogInformation($"Executed method {name} with arguments: {argumentsString}");

            return result;
        }
    }
}
