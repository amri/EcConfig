using NUnit.Framework;
using System.Configuration;
using EcConfig.Core;
using EcConfig.Core.Resources;

namespace EcConfig.Tests
{
    public class GetEcConfigsTests : BaseTest
    {
        #region NUnit methods
        [SetUp]
        public void Init()
        {
            ConfigurationManager.AppSettings[Configurations.Path] = null;//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            ConfigurationManager.AppSettings[Configurations.Filename] = null;//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
        }
        #endregion

        #region Get simple properties

        [Test(Description = "Get default configs if ecconfig.currentConfigFileName not set inside [web/app].config/appSettings")]
        public void GetDefaultProperties()
        {
            Assert.AreEqual("value", Config.Get("key").ToString());
        }

        [Test(Description = "Get configs if ecconfig.currentConfigFileName set to 'dev' inside [web/app].config/appSettings")]
        public void GetDevProperties()
        {
            ConfigurationManager.AppSettings[Configurations.Filename] = "dev";//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            Assert.AreEqual("dev", Config.Get("key").ToString());
        }

        [Test(Description = "Get configs if ecconfig.currentConfigFileName set to 'qa' inside [web/app].config/appSettings")]
        public void GetQaProperties()
        {
            ConfigurationManager.AppSettings[Configurations.Filename] = "qa";//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            Assert.AreEqual("qa", Config.Get("key").ToString());
        }

        #endregion

        #region Get properties inside specific folder

        [Test(Description = "Get dev configs inside configs folder")]
        public void GetDevPropertiesInsideConfigsFolder()
        {
            ConfigurationManager.AppSettings[Configurations.Path] = "configs/";//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            ConfigurationManager.AppSettings[Configurations.Filename] = "dev";//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            Assert.AreEqual("devInsideConfigsFolder", Config.Get("key").ToString());
        }

        #endregion
    }
}
