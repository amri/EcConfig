using NUnit.Framework;
using System;
using System.Linq;
using System.Runtime.Caching;
using System.Configuration;

using EcConfig.Core.EcConfiguration;
using EcConfig.Core.Resources;
using EcConfig.Core.Facades;
using EcConfig.Core.Exceptions;

namespace EcConfig.Tests
{
    public class ConfigurationAnalyserTests : BaseTest
    {
        [SetUp]
        public void Init()
        {
            ConfigurationManager.AppSettings[ConfigurationsNames.CurrentConfigFilename] = "dev";
            ConfigurationManager.AppSettings[ConfigurationsNames.ConfigFilesPath] = "./folder";
            ConfigurationManager.AppSettings[ConfigurationsNames.IsCaseSensitive] = "true";
        }

        [Test(Description = "Should return correct config from app/web.config file then insert configs inside cache")]
        public void GetEcConfigConfigurations_NominalCase()
        {
            Assert.IsFalse(MemoryCache.Default.Any(x => x.Key == EcConfigResources.CacheKey_EcConfigs));
            var configs = ConfigurationAnalyser.GetEcConfigConfigurations();
            Assert.AreEqual("./folder", configs.ConfigFilesPath);
            Assert.AreEqual("dev", configs.CurrentConfigFileName);
            Assert.IsTrue(configs.IsCaseSensitive);
            Assert.IsTrue(MemoryCache.Default.Any(x => x.Key == EcConfigResources.CacheKey_EcConfigs));
        }

        [Test]
        public void GetEcConfigConfigurations_ReturnCacheConfigs()
        {
            MemoryCache.Default.Add(EcConfigResources.CacheKey_EcConfigs, new EcGlobalConfigurations { ConfigFilesPath = "a", CurrentConfigFileName = "b", IsCaseSensitive = true }, DateTime.Now.AddDays(1));
            var configs = ConfigurationAnalyser.GetEcConfigConfigurations();
            Assert.AreEqual("a", configs.ConfigFilesPath);
        }

        [Test]
        [ExpectedException(typeof(EcConfigException), ExpectedMessage = "Invalid EcConfig configuration for ecconfig.currentConfigFileName : invalid filename")]
        public void GetEcConfigConfigurations_InvalidCurrentFilename()
        {
            ConfigurationManager.AppSettings[ConfigurationsNames.CurrentConfigFilename] = "²<>test";
            ConfigurationAnalyser.GetEcConfigConfigurations();
        }

        [Test]
        [ExpectedException(typeof(EcConfigException), ExpectedMessage = "Invalid EcConfig configuration for ecconfig.configFilesPath : invalid path")]
        public void GetEcConfigConfigurations_InvalidFilesPath()
        {
            ConfigurationManager.AppSettings[ConfigurationsNames.ConfigFilesPath] = "<test/ok";
            ConfigurationAnalyser.GetEcConfigConfigurations();
        }

        [Test]
        [ExpectedException(typeof(EcConfigException), ExpectedMessage = "Invalid EcConfig configuration for ecconfig.isCaseSensitive : should be true or false")]
        public void GetEcConfigConfigurations_InvalidIsCasSensitive()
        {
            ConfigurationManager.AppSettings[ConfigurationsNames.IsCaseSensitive] = "truee";
            ConfigurationAnalyser.GetEcConfigConfigurations();
        }
    }
}
