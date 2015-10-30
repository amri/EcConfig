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
                var configCurrentConfigFileName = ConfigurationManager.AppSettings[Configurations.Filename];
                if (!string.IsNullOrEmpty(configCurrentConfigFileName))
                {
                    if (configCurrentConfigFileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
                    {
                        throw new EcConfigException(string.Format(Errors.ConfigCurrentConfigFileName, Configurations.Filename), null);
                    }
                    configs.Filename = configCurrentConfigFileName;
                }

                //Files path
                var configFilesPath = ConfigurationManager.AppSettings[Configurations.Path];
                if (!string.IsNullOrEmpty(configFilesPath))
                {
                    if (configFilesPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
                    {
                        throw new EcConfigException(string.Format(Errors.ConfigConfigFilesPath, Configurations.Path), null);
                    }
                    configs.Path = configFilesPath;
                }

                //Case sensitive
                var configIsCaseSensitive = ConfigurationManager.AppSettings[Configurations.IsCaseSensitive];
                if (!string.IsNullOrEmpty(configIsCaseSensitive))
                {
                    bool isCaseSensitive;
                    if (!Boolean.TryParse(configIsCaseSensitive, out isCaseSensitive))
                    {
                        throw new EcConfigException(string.Format(Errors.ConfigIsCaseSensitive, Configurations.IsCaseSensitive), null);
                    }
                    configs.IsCaseSensitive = isCaseSensitive;
                }

                CacheManager.Add(EcConfigResources.CacheKey_EcConfigs, configs);
            }
            return configs;
        }
    }
}
