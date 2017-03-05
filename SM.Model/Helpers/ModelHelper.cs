using SM.Cache;
using System;
using System.Linq;
using System.Reflection;

namespace SM.Model
{
    public static class ModelHelper
    {
        public static string GetRepresentTable(this object model)
        {
            string res = string.Empty;
            var att = model.GetAttribute<MapTableAttribute>();
            if (att != null)
            {
                var tblAttr = att as MapTableAttribute;
                res = tblAttr.Name;
            }
            return res;
        }
        /// <summary>
        /// Get attribute applied to class 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">Object instance applied attribute</param>
        /// <returns>Attribute object if found, otherwise null</returns>
        public static T GetAttribute<T>(this object model) where T : Attribute
        {
            var atts = model.GetType().GetCustomAttributes(typeof(T), false);
            T res = null;
            if (atts != null && atts.Any())
            {
                res = atts.First() as T;
            }
            return res;
        }
        /// <summary>
        /// Get attribute applied to property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">Object instance contained property</param>
        /// <param name="propertyName">Property name</param>
        /// <returns>Attribute object if found, otherwise null</returns>
        public static T GetAttribute<T>(this object model, string propertyName) where T : Attribute
        {
            PropertyInfo[] props = model.GetType().GetProperties();
            PropertyInfo prop = props.FirstOrDefault(
                x => x.Name.Equals(propertyName,
                     StringComparison.OrdinalIgnoreCase));
            T res = null;
            if (prop != null)
            {
                res = prop.GetCustomAttribute<T>();
            }
            return res;
        }
        public static PropertyInfo GetFirstPropertyAppliedAttribute<T>(this object obj) where T: Attribute
        {
            var props = CacheManager.Instance.GetCachedProperties(obj.GetType());
            PropertyInfo res = null;
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetCustomAttribute<T>() != null)
                {
                    res = prop;
                    break;
                }
            }
            return res;
        }
    }
}