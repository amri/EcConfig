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
            Filename = "default";
            Path = string.Empty;
            IsCaseSensitive = true;
        }

        public string Filename { get; set; }
        public string Path { get; set; }
        public bool IsCaseSensitive { get; set; }
    }
}
