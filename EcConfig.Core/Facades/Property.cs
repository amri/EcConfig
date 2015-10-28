using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcConfig.Core.Facades
{
    public class Property
    {
        private string _property;

        public Property(string property)
        {
            _property = property;
        }

        public static implicit operator Property(string property)
        {
            // While not technically a requirement; see below why this is done.
            if (property == null)
                return null;

            return new Property(property);
        }

        public static implicit operator string(Property property)
        {
            if (property == null)
                return null;

            return property._property;
        }

        public override string ToString()
        {
            return _property;
        }

        public int ToInt()
        {
            var value = 0;
            Int32.TryParse(_property, out value);
            return value;
        }
    }
}
