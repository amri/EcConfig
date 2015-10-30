using NUnit.Framework;
using EcConfig.Core.Facades;


namespace EcConfig.Tests
{
    public class EcGlobalConfigurationsTests : BaseTest
    {
        [Test]
        public void DefaultEcConfigConfigurations()
        {
            var configs = new EcGlobalConfigurations();
            Assert.AreEqual("default", configs.Filename);
            Assert.AreEqual("", configs.Path);
            Assert.IsTrue(configs.IsCaseSensitive);
        }
    }
}
