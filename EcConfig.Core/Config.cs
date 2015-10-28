using System;
using System.Linq;

using EcConfig.Core.Facades;
using EcConfig.Core.EcConfiguration;
using EcConfig.Core.Exceptions;
using EcConfig.Core.Resources;


namespace EcConfig.Core
{
    public static class Config
    {
        #region public methods
        public static Property Get(string key){
            //Get EcConfigs
            var ecConfs = ConfigurationAnalyser.GetEcConfigConfigurations();
            //Get client config properties
            var potProperty = PropertiesExtractor.GetProperties(ecConfs).FirstOrDefault(x => x.Key.Equals(key));
            //Return result or empty string
            return potProperty.Key == null ? new Property(string.Empty) : new Property(potProperty.Value);
        }
        #endregion
    }
}
