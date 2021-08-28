using System.Globalization;

namespace WordPressPCL.Utility
{
    /// <summary>
    /// Helper class with common methods to operate with MIME Type
    /// </summary>
    public static class MimeTypeHelper
    {
        /// <summary>
        /// Get MIME type of file from extension
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetMIMETypeFromExtension(string extension)
        {
            //List from https://codex.wordpress.org/Function_Reference/get_allowed_mime_types
            return (extension?.ToLower(CultureInfo.InvariantCulture)) switch
            {
                // Image formats
                "jpg" or "jpeg" or "jpe" => "image/jpeg",
                "gif" => "image/gif",
                "png" => "image/png",
                "bmp" => "image/bmp",
                "tif" or "tiff" => "image/tiff",
                "ico" => "image/x-icon",

                // Video formats
                "asf" or "asx" => "video/x-ms-asf",
                "wmv" => "video/x-ms-wmv",
                "wmx" => "video/x-ms-wmx",
                "wm" => "video/x-ms-wm",
                "avi" => "video/avi",
                "divx" => "video/divx",
                "flv" => "video/x-flv",
                "mov" or "qt" => "video/quicktime",
                "mpeg" or "mpg" or "mpe" => "video/mpeg",
                "mp4" or "m4v" => "video/mp4",
                "ogv" => "video/ogg",
                "webm" => "video/webm",
                "mkv" => "video/x-matroska",

                // Text formats
                "txt" or "asc" or "c" or "cc" or "h" => "text/plain",
                "csv" => "text/csv",
                "tsv" => "text/tab-separated-values",
                "ics" => "text/calendar",
                "rtx" => "text/richtext",
                "css" => "text/css",
                "htm" or "html" => "text/html",

                // Audio formats
                "mp3" or "m4a" or "m4b" => "audio/mpeg",
                "ra" or "ram" => "audio/x-realaudio",
                "wav" => "audio/wav",
                "ogg" or "oga" => "audio/ogg",
                "mid" or "midi" => "audio/midi",
                "wma" => "audio/x-ms-wma",
                "wax" => "audio/x-ms-wax",
                "mka" => "audio/x-matroska",

                // Misc application formats
                "rtf" => "application/rtf",
                "js" => "application/javascript",
                "pdf" => "application/pdf",
                "swf" => "application/x-shockwave-flash",
                "class" => "application/java",
                "tar" => "application/x-tar",
                "zip" => "application/zip",
                "gz" or "gzip" => "application/x-gzip",
                "rar" => "application/rar",
                "7z" => "application/x-7z-compressed",
                "exe" => "application/x-msdownload",

                // MS Office formats
                "doc" => "application/msword",
                "pot" or "pps" or "ppt" => "application/vnd.ms-powerpoint",
                "wri" => "application/vnd.ms-write",
                "xla" or "xls" or "xlt" or "xlw" => "application/vnd.ms-excel",
                "mdb" => "application/vnd.ms-access",
                "mpp" => "application/vnd.ms-project",
                "docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "docm" => "application/vnd.ms-word.document.macroEnabled.12",
                "dotx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.template",
                "dotm" => "application/vnd.ms-word.template.macroEnabled.12",
                "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "xlsm" => "application/vnd.ms-excel.sheet.macroEnabled.12",
                "xlsb" => "application/vnd.ms-excel.sheet.binary.macroEnabled.12",
                "xltx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.template",
                "xltm" => "application/vnd.ms-excel.template.macroEnabled.12",
                "xlam" => "application/vnd.ms-excel.addin.macroEnabled.12",
                "pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                "pptm" => "application/vnd.ms-powerpoint.presentation.macroEnabled.12",
                "ppsx" => "application/vnd.openxmlformats-officedocument.presentationml.slideshow",
                "ppsm" => "application/vnd.ms-powerpoint.slideshow.macroEnabled.12",
                "potx" => "application/vnd.openxmlformats-officedocument.presentationml.template",
                "potm" => "application/vnd.ms-powerpoint.template.macroEnabled.12",
                "ppam" => "application/vnd.ms-powerpoint.addin.macroEnabled.12",
                "sldx" => "application/vnd.openxmlformats-officedocument.presentationml.slide",
                "sldm" => "application/vnd.ms-powerpoint.slide.macroEnabled.12",
                "onetoc" or "onetoc2" or "onetmp" or "onepkg" => "application/onenote",

                // OpenOffice formats
                "odt" => "application/vnd.oasis.opendocument.text",
                "odp" => "application/vnd.oasis.opendocument.presentation",
                "ods" => "application/vnd.oasis.opendocument.spreadsheet",
                "odg" => "application/vnd.oasis.opendocument.graphics",
                "odc" => "application/vnd.oasis.opendocument.chart",
                "odb" => "application/vnd.oasis.opendocument.database",
                "odf" => "application/vnd.oasis.opendocument.formula",

                // WordPerfect formats
                "wp" or "wpd" => "application/wordperfect",

                // iWork formats
                "key" => "application/vnd.apple.keynote",
                "numbers" => "application/vnd.apple.numbers",
                "pages" => "application/vnd.apple.pages",

                //Misc Application/octet-stream formats
                "kmz" or "kml" => "application/octet-stream",
                _ => "text/plain",
            };
        }
    }
}