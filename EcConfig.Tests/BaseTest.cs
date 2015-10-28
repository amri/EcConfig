using System.Linq;
using System.Runtime.Caching;
using NUnit.Framework;

namespace EcConfig.Tests
{
    public class BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            //Reset cache
            foreach (var cacheKey in MemoryCache.Default.Select(x => x.Key).ToList())
            {
                MemoryCache.Default.Remove(cacheKey);
            }
        }
    }
}
