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
            switch (extension.ToLower())
            {
                // Image formats
                case "jpg": case "jpeg": case "jpe": return "image/jpeg";
                case "gif": return "image/gif";
                case "png": return "image/png";
                case "bmp": return "image/bmp";
                case "tif": case "tiff": return "image/tiff";
                case "ico": return "image/x-icon";

                // Video formats
                case "asf": case "asx": return "video/x-ms-asf";
                case "wmv": return "video/x-ms-wmv";
                case "wmx": return "video/x-ms-wmx";
                case "wm": return "video/x-ms-wm";
                case "avi": return "video/avi";
                case "divx": return "video/divx";
                case "flv": return "video/x-flv";
                case "mov": case "qt": return "video/quicktime";
                case "mpeg": case "mpg": case "mpe": return "video/mpeg";
                case "mp4": case "m4v": return "video/mp4";
                case "ogv": return "video/ogg";
                case "webm": return "video/webm";
                case "mkv": return "video/x-matroska";

                // Text formats
                case "txt": case "asc": case "c": case "cc": case "h": return "text/plain";
                case "csv": return "text/csv";
                case "tsv": return "text/tab-separated-values";
                case "ics": return "text/calendar";
                case "rtx": return "text/richtext";
                case "css": return "text/css";
                case "htm": case "html": return "text/html";

                // Audio formats
                case "mp3": case "m4a": case "m4b": return "audio/mpeg";
                case "ra": case "ram": return "audio/x-realaudio";
                case "wav": return "audio/wav";
                case "ogg": case "oga": return "audio/ogg";
                case "mid": case "midi": return "audio/midi";
                case "wma": return "audio/x-ms-wma";
                case "wax": return "audio/x-ms-wax";
                case "mka": return "audio/x-matroska";

                // Misc application formats
                case "rtf": return "application/rtf";
                case "js": return "application/javascript";
                case "pdf": return "application/pdf";
                case "swf": return "application/x-shockwave-flash";
                case "class": return "application/java";
                case "tar": return "application/x-tar";
                case "zip": return "application/zip";
                case "gz": case "gzip": return "application/x-gzip";
                case "rar": return "application/rar";
                case "7z": return "application/x-7z-compressed";
                case "exe": return "application/x-msdownload";

                // MS Office formats
                case "doc": return "application/msword";
                case "pot": case "pps": case "ppt": return "application/vnd.ms-powerpoint";
                case "wri": return "application/vnd.ms-write";
                case "xla": case "xls": case "xlt": case "xlw": return "application/vnd.ms-excel";
                case "mdb": return "application/vnd.ms-access";
                case "mpp": return "application/vnd.ms-project";
                case "docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case "docm": return "application/vnd.ms-word.document.macroEnabled.12";
                case "dotx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.template";
                case "dotm": return "application/vnd.ms-word.template.macroEnabled.12";
                case "xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "xlsm": return "application/vnd.ms-excel.sheet.macroEnabled.12";
                case "xlsb": return "application/vnd.ms-excel.sheet.binary.macroEnabled.12";
                case "xltx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.template";
                case "xltm": return "application/vnd.ms-excel.template.macroEnabled.12";
                case "xlam": return "application/vnd.ms-excel.addin.macroEnabled.12";
                case "pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case "pptm": return "application/vnd.ms-powerpoint.presentation.macroEnabled.12";
                case "ppsx": return "application/vnd.openxmlformats-officedocument.presentationml.slideshow";
                case "ppsm": return "application/vnd.ms-powerpoint.slideshow.macroEnabled.12";
                case "potx": return "application/vnd.openxmlformats-officedocument.presentationml.template";
                case "potm": return "application/vnd.ms-powerpoint.template.macroEnabled.12";
                case "ppam": return "application/vnd.ms-powerpoint.addin.macroEnabled.12";
                case "sldx": return "application/vnd.openxmlformats-officedocument.presentationml.slide";
                case "sldm": return "application/vnd.ms-powerpoint.slide.macroEnabled.12";
                case "onetoc": case "onetoc2": case "onetmp": case "onepkg": return "application/onenote";

                // OpenOffice formats
                case "odt": return "application/vnd.oasis.opendocument.text";
                case "odp": return "application/vnd.oasis.opendocument.presentation";
                case "ods": return "application/vnd.oasis.opendocument.spreadsheet";
                case "odg": return "application/vnd.oasis.opendocument.graphics";
                case "odc": return "application/vnd.oasis.opendocument.chart";
                case "odb": return "application/vnd.oasis.opendocument.database";
                case "odf": return "application/vnd.oasis.opendocument.formula";

                // WordPerfect formats
                case "wp": case "wpd": return "application/wordperfect";

                // iWork formats
                case "key": return "application/vnd.apple.keynote";
                case "numbers": return "application/vnd.apple.numbers";
                case "pages": return "application/vnd.apple.pages";
                default: return "text/plain";
            }
        }
    }
}