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
            ConfigurationManager.AppSettings[Configurations.Filename] = "dev";
            ConfigurationManager.AppSettings[Configurations.Path] = "./folder";
            ConfigurationManager.AppSettings[Configurations.IsCaseSensitive] = "true";
        }

        [Test(Description = "Should return correct config from app/web.config file then insert configs inside cache")]
        public void GetEcConfigConfigurations_NominalCase()
        {
            Assert.IsFalse(MemoryCache.Default.Any(x => x.Key == EcConfigResources.CacheKey_EcConfigs));
            var configs = ConfigurationAnalyser.GetEcConfigConfigurations();
            Assert.AreEqual("./folder", configs.Path);
            Assert.AreEqual("dev", configs.Filename);
            Assert.IsTrue(configs.IsCaseSensitive);
            Assert.IsTrue(MemoryCache.Default.Any(x => x.Key == EcConfigResources.CacheKey_EcConfigs));
        }

        [Test]
        public void GetEcConfigConfigurations_ReturnCacheConfigs()
        {
            MemoryCache.Default.Add(EcConfigResources.CacheKey_EcConfigs, new EcGlobalConfigurations { Path = "a", Filename = "b", IsCaseSensitive = true }, DateTime.Now.AddDays(1));
            var configs = ConfigurationAnalyser.GetEcConfigConfigurations();
            Assert.AreEqual("a", configs.Path);
        }

        [Test]
        [ExpectedException(typeof(EcConfigException), ExpectedMessage = "Invalid EcConfig configuration for ecconfig.filename : invalid filename")]
        public void GetEcConfigConfigurations_InvalidCurrentFilename()
        {
            ConfigurationManager.AppSettings[Configurations.Filename] = "²<>test";
            ConfigurationAnalyser.GetEcConfigConfigurations();
        }

        [Test]
        [ExpectedException(typeof(EcConfigException), ExpectedMessage = "Invalid EcConfig configuration for ecconfig.path : invalid path")]
        public void GetEcConfigConfigurations_InvalidFilesPath()
        {
            ConfigurationManager.AppSettings[Configurations.Path] = "<test/ok";
            ConfigurationAnalyser.GetEcConfigConfigurations();
        }

        [Test]
        [ExpectedException(typeof(EcConfigException), ExpectedMessage = "Invalid EcConfig configuration for ecconfig.isCaseSensitive : should be true or false")]
        public void GetEcConfigConfigurations_InvalidIsCasSensitive()
        {
            ConfigurationManager.AppSettings[Configurations.IsCaseSensitive] = "truee";
            ConfigurationAnalyser.GetEcConfigConfigurations();
        }
    }
}
