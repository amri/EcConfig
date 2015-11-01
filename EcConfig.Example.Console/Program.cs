using System.Configuration;
using EcConfig.Core;

namespace EcConfig.Example.Console
{
    class Program
    {
        private static void Main(string[] args)
        {
            //Get property from app.config
            System.Console.WriteLine("=== Get property from app.config file: ===");
            System.Console.WriteLine("appSettingsProperty value: {0}", ConfigurationManager.AppSettings["appSettingsProperty"]);

            System.Console.WriteLine();
            //Get properties from default.config thanks to EcConfig (default.config file is the default filename configuration for EcConfig)
            System.Console.WriteLine("=== Get property from default.config file: ===");
            System.Console.WriteLine("rootProperty value: {0}", Config.Get("rootProperty"));
            System.Console.WriteLine("page1/title value: {0}", Config.Get("page1.title"));
            System.Console.WriteLine("page1/part1/title value: {0}", Config.Get("page1.part1.title"));
            System.Console.WriteLine("page1/part2/title value: {0}", Config.Get("page1.part2.title"));
            System.Console.WriteLine("number1 + number2 value: {0}", Config.Get("number1").ToInt() + Config.Get("number2").ToInt());

            System.Console.ReadLine();
        }
    }
}
