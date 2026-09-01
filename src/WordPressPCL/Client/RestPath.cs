using System;
using System.Linq;

namespace WordPressPCL.Client;

internal static class RestPath
{
    public static string EncodeSegments(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        return string.Join("/", value.Split('/').Select(Uri.EscapeDataString));
    }
}
