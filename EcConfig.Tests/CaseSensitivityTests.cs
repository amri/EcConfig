using System.Configuration;
using EcConfig.Core;
using EcConfig.Core.Resources;
using NUnit.Framework;

namespace EcConfig.Tests
{
    public class CaseSensitivityTests : BaseTest
    {
        [TestCase("key", "value", true, Description = "IsCaseSensitive activated (default conf) : key exists")]
        [TestCase("kEy", "", true, Description = "IsCaseSensitive activated (default conf) : kEy doesn't exist")]
        [TestCase("key", "value", false, Description = "IsCaseSensitive not activated (default conf) : key exists")]
        [TestCase("kEy", "value", false, Description = "IsCaseSensitive not activated (default conf) : kEy exists (<=> key)")]
        public void GetDevProperties_CaseSensitiveCases(string key, string expectedValue, bool isCaseSensitive)
        {
            ConfigurationManager.AppSettings[Configurations.IsCaseSensitive] = isCaseSensitive.ToString();
            Assert.AreEqual(expectedValue, Config.Get(key).ToString());
        }
    }
}
