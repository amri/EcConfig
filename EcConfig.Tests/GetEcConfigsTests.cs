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
            ConfigurationManager.AppSettings[ConfigurationsNames.ConfigFilesPath] = null;//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            ConfigurationManager.AppSettings[ConfigurationsNames.CurrentConfigFilename] = null;//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
        }
        #endregion

        #region Get simple configs

        [Test(Description = "Get default configs if ecconfig.currentConfigFileName not set inside [web/app].config/appSettings")]
        public void GetDefaultConfigs()
        {
            Assert.AreEqual("value", Config.Get("key").ToString());
        }

        [Test(Description = "Get configs if ecconfig.currentConfigFileName set to 'dev' inside [web/app].config/appSettings")]
        public void GetDevConfigs()
        {
            ConfigurationManager.AppSettings["ecconfig.currentConfigFileName"] = "dev";//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            Assert.AreEqual("dev", Config.Get("key").ToString());
        }

        [Test(Description = "Get configs if ecconfig.currentConfigFileName set to 'qa' inside [web/app].config/appSettings")]
        public void GetQaConfigs()
        {
            ConfigurationManager.AppSettings["ecconfig.currentConfigFileName"] = "qa";//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            Assert.AreEqual("qa", Config.Get("key").ToString());
        }

        #endregion

        #region Get configs inside specific folder

        [Test(Description = "Get dev configs inside configs folder")]
        public void GetDevConfigsInsideConfigsFolder()
        {
            ConfigurationManager.AppSettings["ecconfig.configFilesPath"] = "configs/";//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            ConfigurationManager.AppSettings["ecconfig.currentConfigFileName"] = "dev";//Override app.config/web.config appSettings configurations. DO NOT USE THIS TRICK FOR REAL APPLICATION
            Assert.AreEqual("devInsideConfigsFolder", Config.Get("key").ToString());
        }

        #endregion
    }
}
