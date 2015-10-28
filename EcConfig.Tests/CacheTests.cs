using System;
using NUnit.Framework;
using System.Runtime.Caching;
using System.Collections.Generic;

using System.Linq;
using EcConfig.Core.Cache;

namespace EcConfig.Tests
{
    public class CacheTests : BaseTest
    {
        [Test(Description = "Add value inside cache")]
        public void AddValueInsideCache()
        {
            CacheManager.Add("key", "value");
            Assert.AreEqual("value", MemoryCache.Default.Get("key"));
        }

        [Test(Description = "Get value inside cache")]
        public void GetValueInsideCache()
        {
            MemoryCache.Default.Add("key", "value", DateTime.Now.AddDays(1));
            Assert.AreEqual("value", CacheManager.Get<string>("key"));
        }

        [Test(Description = "Get complex value inside cache")]
        public void GetComplexValueInsideCache()
        {
            MemoryCache.Default.Add("key", new List<string> { "1", "2" }, DateTime.Now.AddDays(1));
            Assert.AreEqual("1", CacheManager.Get<List<string>>("key").First());
            Assert.AreEqual("2", CacheManager.Get<List<string>>("key").Last());
        }

        [Test(Description = "Test if key exist inside cache")]
        public void KeyExistInsideCache()
        {
            MemoryCache.Default.Add("exist", "", DateTime.Now.AddDays(1));
            Assert.IsTrue(CacheManager.Exist("exist"));
            Assert.IsFalse(CacheManager.Exist("notexist"));
        }

        [Test(Description = "Clear key value inside cache")]
        public void ClearKeyValueInsideCache()
        {
            MemoryCache.Default.Add("key", "value", DateTime.Now.AddDays(1));
            Assert.IsTrue(CacheManager.Exist("key"));
            CacheManager.Clear("key");
            Assert.IsFalse(CacheManager.Exist("key"));
        }
    }
}
