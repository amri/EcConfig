using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcConfig.Core.Exceptions
{
    public class EcConfigException : Exception
    {
        public EcConfigException(string message, Exception innerException): base(message, innerException)
        {}
    }
}
