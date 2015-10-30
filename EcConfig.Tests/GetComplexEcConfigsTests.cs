using NUnit.Framework;
using System.Configuration;
using EcConfig.Core;
using EcConfig.Core.Resources;

namespace EcConfig.Tests
{
    public class GetComplexEcConfigsTests : BaseTest
    {
        #region NUnit methods
        [SetUp]
        public void Init()
        {
            ConfigurationManager.AppSettings[Configurations.Path] = "configs/";//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            ConfigurationManager.AppSettings[Configurations.Filename] = "dev";//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
        }

        [TearDown]
        public void TearDown()
        {
            ConfigurationManager.AppSettings[Configurations.Path] = null;//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            ConfigurationManager.AppSettings[Configurations.Filename] = null;//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
        }
        #endregion

        [Test(Description = "Get configs level 2")]
        public void GetConfigsAtLevel2()
        {
            Assert.AreEqual("LEVEL2", Config.Get("level2.l2el").ToString());
        }

        [Test(Description = "Get configs level 3")]
        public void GetConfigsAtLevel3()
        {
            Assert.AreEqual("LEVEL3", Config.Get("level2.level3.l3el").ToString());
        }

        [Test(Description = "Get configs level 4")]
        public void GetConfigsAtLevel4()
        {
            Assert.AreEqual("LEVEL4", Config.Get("level2.level3.level4.l4el").ToString());
        }
    }
}
