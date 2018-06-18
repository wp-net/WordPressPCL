using Newtonsoft.Json;
using System.Globalization;
using System.Threading.Tasks;

namespace WordPressPCL.Utility
{
    /*
    /// <summary>
    /// Helper class with some usefull functions to operate with date time format settings
    /// </summary>
    public static class DateTimeFormatHelper
    {
        /// <summary>
        /// BETA. Use it only for nonstandard WordPress datetime.
        /// Function tries to convert your PHP WP datetime format  into Microsoft datettime format for serialization/deserialization
        /// Works only with auth
        /// </summary>
        /// <param name="client">current WordPress client instance</param>
        /// <param name="cultureInfo">Your site culture info</param>
        public static async Task AutoConfigureDateSerialization(this WordPressClient client, CultureInfo cultureInfo)
        {
            if (await client.IsValidJWToken())
            {
                var settings = await client.GetSettings();
                var netdateformat = ParsePHPDateTimeFormat(settings.DateFormat, settings.TimeFormat);
                if (client.JsonSerializerSettings == null)
                {
                    client.JsonSerializerSettings = new JsonSerializerSettings() { DateFormatString = netdateformat, Culture = cultureInfo };
                }
                else
                {
                    client.JsonSerializerSettings.DateFormatString = netdateformat;
                }
            }
        }

        private static string ParsePHPDateTimeFormat(string dateFormat, string timeFormat)
        {
            //http://php.net/manual/ru/function.date.php
            //https://msdn.microsoft.com/ru-ru/library/8kb3ddd4(v=vs.110).aspx
            string result = dateFormat.Replace("d", "dd")
                                      .Replace("D", "ddd")
                                      .Replace("j", "d")
                                      .Replace("l", "dddd")
                                      .Replace("M", "MMM")
                                      .Replace("F", "MMMM")
                                      .Replace("m", "MM")
                                      .Replace("n", "M")
                                      .Replace("o", "yyyy")
                                      .Replace("y", "yy")
                                      .Replace("Y", "yyyy") + " " +
                                      timeFormat.Replace("a", "tt")
                                              .Replace("A", "tt")
                                              .Replace("g", "h")
                                              .Replace("G", "H")
                                              .Replace("h", "чч")
                                              .Replace("H", "HH")
                                              .Replace("i", "mm")
                                              .Replace("s", "сс")
                                              .Replace("u", "FFFFFF")
                                              .Replace("v", "FFF")
                                              .Replace("e", "K")
                                              .Replace("O", "zz")
                                              .Replace("P", "zzz");
            return result;
        }
    }*/
}