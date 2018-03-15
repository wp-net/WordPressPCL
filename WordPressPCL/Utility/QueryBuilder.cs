using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using WordPressPCL.Models;

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
        /// <remarks>Default: asc</remarks>
        [QueryText("order")]
        public Order Order { get; set; }

        /// <summary>
        /// include embed info
        /// </summary>
        /// <remarks>Default: false</remarks>
        [QueryText("_embed")]
        public bool Embed { get; set; }

        /// <summary>
        /// Context of request
        /// </summary>
        /// <remarks>Default: view</remarks>
        [QueryText("context")]
        public Context Context { get; set; }

        /// <summary>
        /// Builds the query URL from all properties
        /// </summary>
        /// <returns>query HTTP string</returns>
        public string BuildQueryURL()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var property in this.GetType().GetRuntimeProperties())
            {
                var attribute = property.GetCustomAttribute<QueryTextAttribute>();
                if (attribute != null)
                {
                    var value = GetPropertyValue(property);
                    //var ttt = property.PropertyType.GetTypeInfo().IsEnum;
                    //var ppp = property.GetValue(this);
                    //pass default values
                    if (value is int && (int)value == default(int)) continue;
                    if (value is string && (string.IsNullOrEmpty((string)value) || (string)value == DateTime.MinValue.ToString("yyyy-MM-ddTHH:mm:ss"))) continue;
                    if (value is DateTime && (string)value == DateTime.MinValue.ToString("yyyy-MM-ddTHH:mm:ss")) continue;
                    if (property.PropertyType == typeof(bool) && (string)value == default(bool).ToString().ToLower()) continue;
                    if (property.PropertyType.GetTypeInfo().IsEnum && (int)property.GetValue(this) == 0) continue;
                    //if (property.PropertyType.IsArray && ((Array)value).Length == 0) continue;
                    if (value == null) continue;
                    sb.Append(attribute.Text).Append("=").Append(value).Append("&");
                }
            }
            //insert ? quote to the start of http query text
            if (sb.Length > 0) sb.Insert(0, "?");
            return sb.ToString().TrimEnd('&');
        }

        /// <summary>
        /// Use reflection to get property value
        /// </summary>
        /// <param name="property">PropertyInfo or object</param>
        /// <returns>property value</returns>
        private object GetPropertyValue(object property)
        {
            //secion for propertyInfo object
            if (property is PropertyInfo pi)
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
                        sb.Append(GetPropertyValue(item)).Append(",");
                    }

                    return sb.ToString().TrimEnd(',');
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
            //section for simple object
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