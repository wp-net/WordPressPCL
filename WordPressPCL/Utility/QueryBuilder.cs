using System;
using System.Text;
using System.Reflection;
using WordPressPCL.Models;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Query builder class. Use it for create custom query
    /// </summary>
    public abstract class QueryBuilder
    {
        /// <summary>
        /// Order direction
        /// </summary>
        [QueryText("order")]
        public Order Order { get; set; }
        /// <summary>
        /// include embed info
        /// </summary>
        [QueryText("_embed")]
        public bool Embed { get; set; }
        /// <summary>
        /// Context of request
        /// </summary>
        [QueryText("context")]
        public Context Context { get; set; }
        /// <summary>
        /// Builds the query URL from all properties
        /// </summary>
        /// <returns>query HTTP string</returns>
        public string BuildQueryURL() 
        {
            StringBuilder sb = new StringBuilder();
            foreach(var property in this.GetType().GetRuntimeProperties())
            {
                var attribute=property.GetCustomAttribute<QueryTextAttribute>();
                if (attribute != null)
                {
                    var value = GetPropertyValue(property);
                    if (value is int && (int)value==default(int)) continue;
                    if (value is string && ((string)value == string.Empty || (string)value == DateTime.MinValue.ToString("yyyy-MM-ddTHH:mm:ss"))) continue;
                    if (value is DateTime && (string)value == DateTime.MinValue.ToString("yyyy-MM-ddTHH:mm:ss")) continue;
                    if (value is bool && (bool)value == default(bool)) continue;
                    if (value is Enum && (int)value == 0) continue;
                    if (property.GetType().IsArray && ((Array)value).Length == 0) continue;
                    if (value == null) continue;
                    sb.Append($"{attribute.Text}={value}&");
                }   
            }
            return sb.ToString();
        }
        /// <summary>
        /// Use reflection to get property value
        /// </summary>
        /// <param name="property">PropertyInfo or object</param>
        /// <returns>property value</returns>
        private object GetPropertyValue(object property)
        {
            PropertyInfo pi = property as PropertyInfo;
            if (pi != null)
            {
                
                if (pi.PropertyType.GetTypeInfo().IsEnum)
                {
                    var attribute = pi.PropertyType.GetRuntimeField(((Enum)pi.GetValue(this)).ToString()).GetCustomAttribute<EnumMemberAttribute>();// .GetType().GetRuntimeProperties().First().GetCustomAttribute<EnumMemberAttribute>();

                    return attribute.Value;
                }
                if (pi.PropertyType.IsArray)
                {
                    var array = (Array)pi.GetValue(this);
                    if (array == null) return null;
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in array)
                    {
                        sb.Append($"{GetPropertyValue(item)},");
                    }
                    return sb.ToString();
                }
                if (pi.PropertyType == typeof(DateTime))
                {
                    return ((DateTime)pi.GetValue(this)).ToString("yyyy-MM-ddTHH:mm:ss");
                }
                if (pi.PropertyType == typeof(bool))
                {
                    return ((bool)pi.GetValue(this)).ToString().ToLower();
                }
                return pi.GetValue(this);
            }
            else
            {
                if (property.GetType().GetTypeInfo().IsEnum)
                {
                    var attribute = property.GetType().GetRuntimeField(((Enum)property).ToString()).GetCustomAttribute<EnumMemberAttribute>();
                    return attribute.Value;
                }
                if (property.GetType() == typeof(DateTime))
                {
                    return ((DateTime)property).ToString("yyyy-MM-ddTHH:mm:ss");
                }
                if (property.GetType() == typeof(bool))
                {
                    return ((bool)property).ToString().ToLower();
                }
                return property;
            }

        }
    }
}
