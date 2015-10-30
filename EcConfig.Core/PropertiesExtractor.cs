using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using EcConfig.Core.Exceptions;
using EcConfig.Core.Facades;
using EcConfig.Core.Resources;
using EcConfig.Core.Cache;

namespace EcConfig.Core
{
    public class PropertiesExtractor
    {
        /// <summary>
        /// Get properties
        /// from cache or by extracting from config file
        /// </summary>
        /// <param name="ecConfs"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetProperties(EcGlobalConfigurations ecConfs)
        {
            var properties = new Dictionary<string, string>();
            if (!CacheManager.Exist(EcConfigResources.CacheKey_Properties))
            {
                var path = string.Format(EcConfigResources.ConfigFileNameFormat,
                    Path.Combine(ecConfs.Path, ecConfs.Filename));
                XDocument doc;
                try
                {
                    doc = XDocument.Load(path);
                }
                catch (Exception e)
                {
                    throw new EcConfigException(string.Format(Errors.PropertiesExtractorConfigFileDoesNotExist, path), e);
                }
                if (doc.Root == null)
                    throw new EcConfigException(string.Format(Errors.PropertiesExtractorSystemNotToExtractProperties, path), null);

                ExtractProperties(doc.Root.Elements(), properties, 1, new List<string>(), ecConfs.IsCaseSensitive);

                CacheManager.Add(EcConfigResources.CacheKey_Properties, properties);
            }
            properties = CacheManager.Get<Dictionary<string, string>>(EcConfigResources.CacheKey_Properties);

            return properties;
        }

        /// <summary>
        /// Extract properties from enumerable of xml elements
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="properties"></param>
        /// <param name="level"></param>
        /// <param name="parents"></param>
        /// <param name="isCaseSensitive"></param>
        private static void ExtractProperties(IEnumerable<XElement> elements, Dictionary<string, string> properties, int level, List<string> parents, bool isCaseSensitive)
        {
            foreach (var el in elements)
            {
                if (el.Name == EcConfigResources.PropertyTag)
                {
                    var parentPath = string.Empty;
                    if (parents.Count > 0)
                        parentPath = string.Join(EcConfigResources.PropertyJoinCharacter, parents) + EcConfigResources.PropertyJoinCharacter;

                    var key = parentPath + el.Attribute(XName.Get(EcConfigResources.KeyAttribute)).Value;
                    key = isCaseSensitive ? key : key.ToLower();

                    properties.Add(key, el.Attribute(XName.Get(EcConfigResources.ValueAttribute)).Value);
                }
                else
                {
                    if (level <= Convert.ToInt32(EcConfigResources.RecursiveLevel))
                    {
                        parents.Add(el.Name.ToString());
                        ExtractProperties(el.Elements(), properties, level++, parents, isCaseSensitive);
                        parents.Remove(el.Name.ToString());
                    }
                    else
                        throw new EcConfigException(string.Format(EcConfigResources.RecursiveLevelReached, EcConfigResources.RecursiveLevel), null);
                }
            }
        }
    }
}
