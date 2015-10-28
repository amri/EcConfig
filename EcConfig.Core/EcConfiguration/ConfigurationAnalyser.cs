using System;
using System.Configuration;
using System.IO;

using EcConfig.Core.Facades;
using EcConfig.Core.Resources;
using EcConfig.Core.Cache;
using EcConfig.Core.Exceptions;

namespace EcConfig.Core.EcConfiguration
{
    public class ConfigurationAnalyser
    {
        public static EcGlobalConfigurations GetEcConfigConfigurations()
        {
            var configs = CacheManager.Get<EcGlobalConfigurations>(EcConfigResources.CacheKey_EcConfigs);
            if (configs == null)
            {
                configs = new EcGlobalConfigurations();

                //Filename
                var configCurrentConfigFileName = ConfigurationManager.AppSettings[ConfigurationsNames.CurrentConfigFilename];
                if (!string.IsNullOrEmpty(configCurrentConfigFileName))
                {
                    if (configCurrentConfigFileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                    {
                        throw new EcConfigException(string.Format(Errors.ConfigCurrentConfigFileName, ConfigurationsNames.CurrentConfigFilename), null);
                    }
                    configs.CurrentConfigFileName = configCurrentConfigFileName;
                }

                //Files path
                var configFilesPath = ConfigurationManager.AppSettings[ConfigurationsNames.ConfigFilesPath];
                if (!string.IsNullOrEmpty(configFilesPath))
                {
                    if (configFilesPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                    {
                        throw new EcConfigException(string.Format(Errors.ConfigConfigFilesPath, ConfigurationsNames.ConfigFilesPath), null);
                    }
                    configs.ConfigFilesPath = configFilesPath;
                }

                //Case sensitive
                var configIsCaseSensitive = ConfigurationManager.AppSettings[ConfigurationsNames.IsCaseSensitive];
                if (!string.IsNullOrEmpty(configIsCaseSensitive))
                {
                    bool isCaseSensitive;
                    if (!Boolean.TryParse(configIsCaseSensitive, out isCaseSensitive))
                    {
                        throw new EcConfigException(string.Format(Errors.ConfigIsCaseSensitive, ConfigurationsNames.IsCaseSensitive), null);
                    }
                    configs.IsCaseSensitive = isCaseSensitive;
                }

                CacheManager.Add(EcConfigResources.CacheKey_EcConfigs, configs);
            }
            return configs;
        }
    }
}
