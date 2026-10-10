using System;
using System.Linq;

namespace WordPressPCL.Client;

internal static class RestPath
{
    public static string EncodeSegments(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        string[] segments = value.Split('/');
        if (segments.Any(segment => segment is "." or ".."))
        {
            throw new ArgumentException("Path segments cannot be '.' or '..'.", nameof(value));
        }

        return string.Join("/", segments.Select(Uri.EscapeDataString));
    }
}
