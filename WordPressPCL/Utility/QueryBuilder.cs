using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
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
        public string BuildQuery()
        {
            NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
            foreach (PropertyInfo property in GetType().GetRuntimeProperties())
            {
                QueryTextAttribute attribute = property.GetCustomAttribute<QueryTextAttribute>();
                ExcludeQueryTextAttribute exclusionAttribute = property.GetCustomAttribute<ExcludeQueryTextAttribute>();
                if (attribute != null)
                {
                    object value = GetPropertyValue(property);

                    if (value is null) continue;
                    if (exclusionAttribute != null && value.ToString().ToLowerInvariant() == exclusionAttribute.ExclusionValue) continue;

                    //pass default values
                    if (value is int valueInt && valueInt == default) continue;
                    if (value is string valueString && (string.IsNullOrEmpty(valueString) || valueString == DateTime.MinValue.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture))) continue;
                    if (value is DateTime valueDateTime && valueDateTime == default) continue;
                    if (property.PropertyType == typeof(bool) && (string)value == default(bool).ToString().ToLowerInvariant()) continue;
                    query.Add(attribute.Text, value.ToString().ToLowerInvariant());
                }
            }
            string queryString = query.ToString();
            if (queryString.Length > 0)
            {
                queryString = "?" + queryString;
            }
            return queryString;
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
                    EnumMemberAttribute attribute = pi.PropertyType.GetRuntimeField(((Enum)pi.GetValue(this)).ToString()).GetCustomAttribute<EnumMemberAttribute>();

                    return attribute.Value;
                }
                if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    Type genericParamOfList = pi.PropertyType.GetGenericArguments()[0];
                    Type finalListType = typeof(List<>).MakeGenericType(genericParamOfList);
                    dynamic list = Convert.ChangeType(pi.GetValue(this), finalListType, CultureInfo.InvariantCulture);
                    if (list == null) return null;
                    StringBuilder sb = new();
                    foreach (dynamic item in list)
                    {
                        sb.Append(GetPropertyValue((object)item)).Append(',');
                    }
                    return sb.ToString().TrimEnd(',');
                }
                if (pi.PropertyType == typeof(DateTime))
                {
                    return ((DateTime)pi.GetValue(this)).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                }
                if (pi.PropertyType == typeof(bool))
                {
                    return ((bool)pi.GetValue(this)).ToString().ToLowerInvariant();
                }
                return pi.GetValue(this);
            }
            //section for simple object
            else
            {
                if (property.GetType().GetTypeInfo().IsEnum)
                {
                    EnumMemberAttribute attribute = property.GetType().GetRuntimeField(((Enum)property).ToString()).GetCustomAttribute<EnumMemberAttribute>();
                    return attribute.Value;
                }
                if (property.GetType() == typeof(DateTime))
                {
                    return ((DateTime)property).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
                }
                if (property.GetType() == typeof(bool))
                {
                    return ((bool)property).ToString().ToLowerInvariant();
                }
                return property;
            }
        }
    }
}
