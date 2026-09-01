
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WordPressPCL.Models;

/// <summary>
/// Details of media item
/// <see cref="MediaItem.MediaDetails"/>
/// </summary>
public class MediaDetails
{
    /// <summary>
    /// Media width
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Width { get; set; }
    /// <summary>
    /// Media height
    /// </summary>
    [JsonPropertyName("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Height { get; set; }
    /// <summary>
    /// File
    /// </summary>
    [JsonPropertyName("file")]
    public string? File { get; set; }
    /// <summary>
    /// Sizes
    /// </summary>
    [JsonPropertyName("sizes")]
    public IDictionary<string, MediaSize>? Sizes { get; set; }
    /// <summary>
    /// Meta info of Image
    /// </summary>
    [JsonPropertyName("image_meta")]
    public ImageMeta? ImageMeta { get; set; }
    /// <summary>
    /// Audio data format
    /// </summary>
    [JsonPropertyName("dataformat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DataFormat { get; set; }
    /// <summary>
    /// Number of audio channels
    /// </summary>
    [JsonPropertyName("channels")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Channels { get; set; }
    /// <summary>
    /// Audio sample rate in hertz
    /// </summary>
    [JsonPropertyName("sample_rate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? SampleRate { get; set; }
    /// <summary>
    /// Audio bitrate in bits per second
    /// </summary>
    [JsonPropertyName("bitrate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Bitrate { get; set; }
    /// <summary>
    /// Audio channel mode
    /// </summary>
    [JsonPropertyName("channelmode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ChannelMode { get; set; }
    /// <summary>
    /// Audio bitrate mode
    /// </summary>
    [JsonPropertyName("bitrate_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BitrateMode { get; set; }
    /// <summary>
    /// Whether the audio encoding is lossless
    /// </summary>
    [JsonPropertyName("lossless")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Lossless { get; set; }
    /// <summary>
    /// Audio encoder options
    /// </summary>
    [JsonPropertyName("encoder_options")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EncoderOptions { get; set; }
    /// <summary>
    /// Audio compression ratio
    /// </summary>
    [JsonPropertyName("compression_ratio")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? CompressionRatio { get; set; }
    /// <summary>
    /// Media file format
    /// </summary>
    [JsonPropertyName("fileformat")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FileFormat { get; set; }
    /// <summary>
    /// Media file size in bytes
    /// </summary>
    [JsonPropertyName("filesize")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? FileSize { get; set; }
    /// <summary>
    /// Media MIME type
    /// </summary>
    [JsonPropertyName("mime_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MimeType { get; set; }
    /// <summary>
    /// Audio duration in seconds
    /// </summary>
    [JsonPropertyName("length")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Length { get; set; }
    /// <summary>
    /// Formatted audio duration
    /// </summary>
    [JsonPropertyName("length_formatted")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LengthFormatted { get; set; }
    /// <summary>
    /// Audio genre
    /// </summary>
    [JsonPropertyName("genre")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Genre { get; set; }
    /// <summary>
    /// Audio release year
    /// </summary>
    [JsonPropertyName("year")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Year { get; set; }
    /// <summary>
    /// Audio tag date
    /// </summary>
    [JsonPropertyName("date")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Date { get; set; }
    /// <summary>
    /// Audio tag text
    /// </summary>
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }
    /// <summary>
    /// Audio title
    /// </summary>
    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }
    /// <summary>
    /// Audio artist
    /// </summary>
    [JsonPropertyName("artist")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Artist { get; set; }
    /// <summary>
    /// Audio album
    /// </summary>
    [JsonPropertyName("album")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Album { get; set; }
    /// <summary>
    /// Media creation time as a Unix timestamp
    /// </summary>
    [JsonPropertyName("created_timestamp")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? CreatedTimestamp { get; set; }
}
