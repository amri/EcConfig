using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcConfig.Core.Facades
{
    public class EcGlobalConfigurations
    {
        public EcGlobalConfigurations()
        {
            //Default configurations
            CurrentConfigFileName = "default";
            ConfigFilesPath = string.Empty;
            IsCaseSensitive = true;
        }

        public string CurrentConfigFileName { get; set; }
        public string ConfigFilesPath { get; set; }
        public bool IsCaseSensitive { get; set; }
    }
}
