using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Diagnostics;
using EcConfig.Core;

namespace EcConfig.Example.Console
{
    class Program
    {
        private const int Iterations = 10000;

        private static void Main(string[] args)
        {
            var results = new List<long>();
            var value = string.Empty;

            for (var i = 1; i < Iterations; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                value = ConfigurationManager.AppSettings["Test"];
                stopwatch.Stop();
                results.Add(stopwatch.ElapsedMilliseconds);
            }
            System.Console.WriteLine(@"Average response time by get property from app.config: {0} for value: {1}", results.Sum()/Iterations, value);


            var ecResults = new List<long>();
            var ecValue = string.Empty;

            for (var i = 1; i < Iterations; i++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                ecValue = Config.Get("key");
                stopwatch.Stop();
                ecResults.Add(stopwatch.ElapsedMilliseconds);
            }
            System.Console.WriteLine(@"Average response time by get property from app.config: {0} for value: {1}", ecResults.Sum()/Iterations, ecValue);


            System.Console.WriteLine(Config.Get("int").ToInt() + Config.Get("int2").ToInt());
            System.Console.WriteLine(Config.Get("test.key2"));

            System.Console.WriteLine(Config.Get("test.ok.great"));
            System.Console.WriteLine(Config.Get("end"));

            System.Console.ReadLine();
        }
    }
}
