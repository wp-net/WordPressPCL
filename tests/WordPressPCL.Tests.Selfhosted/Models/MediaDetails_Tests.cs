using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text.Json;
using WordPressPCL.Models;

namespace WordPressPCL.Tests.Selfhosted.Models;

[TestClass]
public class MediaDetails_Tests
{
    private static readonly string[] s_audioPropertyNames =
    [
        "dataformat",
        "channels",
        "sample_rate",
        "bitrate",
        "channelmode",
        "bitrate_mode",
        "lossless",
        "encoder_options",
        "compression_ratio",
        "fileformat",
        "filesize",
        "mime_type",
        "length",
        "length_formatted",
        "genre",
        "year",
        "date",
        "text",
        "title",
        "artist",
        "album",
        "created_timestamp",
    ];

    private const string AudioMediaItem = """
    {
        "mime_type": "audio/mpeg",
        "media_details": {
            "dataformat": "mp3",
            "channels": 2,
            "sample_rate": 44100,
            "bitrate": 256000,
            "channelmode": "stereo",
            "bitrate_mode": "cbr",
            "lossless": false,
            "encoder_options": "CBR256",
            "compression_ratio": 0.18140589569160998,
            "fileformat": "mp3",
            "filesize": 1545929,
            "mime_type": "audio/mpeg",
            "length": 48,
            "length_formatted": "0:48",
            "genre": "Blues",
            "year": "2019",
            "date": "0813",
            "text": "Author name",
            "title": "Example track",
            "artist": "Example artist",
            "album": "Example album",
            "created_timestamp": 1565654400,
            "sizes": {}
        }
    }
    """;

    [TestMethod]
    public void AudioMetadata_DeserializesWordPressPayload()
    {
        MediaItem? mediaItem = JsonSerializer.Deserialize<MediaItem>(AudioMediaItem);

        Assert.IsNotNull(mediaItem);
        Assert.AreEqual("audio/mpeg", mediaItem.MimeType);
        Assert.IsNotNull(mediaItem.MediaDetails);
        Assert.AreEqual("mp3", mediaItem.MediaDetails.DataFormat);
        Assert.AreEqual(2, mediaItem.MediaDetails.Channels);
        Assert.AreEqual(44100, mediaItem.MediaDetails.SampleRate);
        Assert.AreEqual(256000D, mediaItem.MediaDetails.Bitrate);
        Assert.AreEqual("stereo", mediaItem.MediaDetails.ChannelMode);
        Assert.AreEqual("cbr", mediaItem.MediaDetails.BitrateMode);
        Assert.AreEqual(false, mediaItem.MediaDetails.Lossless);
        Assert.AreEqual("CBR256", mediaItem.MediaDetails.EncoderOptions);
        Assert.AreEqual(0.18140589569160998D, mediaItem.MediaDetails.CompressionRatio);
        Assert.AreEqual("mp3", mediaItem.MediaDetails.FileFormat);
        Assert.AreEqual(1545929L, mediaItem.MediaDetails.FileSize);
        Assert.AreEqual("audio/mpeg", mediaItem.MediaDetails.MimeType);
        Assert.AreEqual(48, mediaItem.MediaDetails.Length);
        Assert.AreEqual("0:48", mediaItem.MediaDetails.LengthFormatted);
        Assert.AreEqual("Blues", mediaItem.MediaDetails.Genre);
        Assert.AreEqual("2019", mediaItem.MediaDetails.Year);
        Assert.AreEqual("0813", mediaItem.MediaDetails.Date);
        Assert.AreEqual("Author name", mediaItem.MediaDetails.Text);
        Assert.AreEqual("Example track", mediaItem.MediaDetails.Title);
        Assert.AreEqual("Example artist", mediaItem.MediaDetails.Artist);
        Assert.AreEqual("Example album", mediaItem.MediaDetails.Album);
        Assert.AreEqual(1565654400L, mediaItem.MediaDetails.CreatedTimestamp);
        Assert.IsNotNull(mediaItem.MediaDetails.Sizes);
        Assert.AreEqual(0, mediaItem.MediaDetails.Sizes.Count);
    }

    [TestMethod]
    public void AudioMetadata_SerializesUsingWordPressPropertyNames()
    {
        MediaItem? mediaItem = JsonSerializer.Deserialize<MediaItem>(AudioMediaItem);
        Assert.IsNotNull(mediaItem);
        Assert.IsNotNull(mediaItem.MediaDetails);

        using JsonDocument document = JsonDocument.Parse(JsonSerializer.Serialize(mediaItem.MediaDetails));
        JsonElement root = document.RootElement;

        foreach (string propertyName in s_audioPropertyNames)
        {
            Assert.IsTrue(root.TryGetProperty(propertyName, out _), $"Expected serialized property '{propertyName}'.");
        }

        Assert.AreEqual(44100, root.GetProperty("sample_rate").GetInt32());
        Assert.AreEqual("cbr", root.GetProperty("bitrate_mode").GetString());
        Assert.AreEqual("0:48", root.GetProperty("length_formatted").GetString());
        Assert.AreEqual(1565654400L, root.GetProperty("created_timestamp").GetInt64());
        Assert.IsFalse(root.TryGetProperty("SampleRate", out _));
        Assert.IsFalse(root.TryGetProperty("BitrateMode", out _));
        Assert.IsFalse(root.TryGetProperty("LengthFormatted", out _));
        Assert.IsFalse(root.TryGetProperty("CreatedTimestamp", out _));
    }

    [TestMethod]
    public void ImageMetadata_DoesNotSerializeUnsetAudioFields()
    {
        MediaDetails details = new()
        {
            Width = 1024,
            Height = 768,
            File = "2026/09/example.jpg",
            Sizes = new Dictionary<string, MediaSize>(),
            ImageMeta = new ImageMeta { Camera = "Example camera" },
        };

        using JsonDocument document = JsonDocument.Parse(JsonSerializer.Serialize(details));
        JsonElement root = document.RootElement;

        Assert.AreEqual(1024, root.GetProperty("width").GetInt32());
        Assert.AreEqual(768, root.GetProperty("height").GetInt32());
        Assert.AreEqual("2026/09/example.jpg", root.GetProperty("file").GetString());
        Assert.IsTrue(root.TryGetProperty("sizes", out _));
        Assert.AreEqual("Example camera", root.GetProperty("image_meta").GetProperty("camera").GetString());

        foreach (string propertyName in s_audioPropertyNames)
        {
            Assert.IsFalse(root.TryGetProperty(propertyName, out _), $"Did not expect audio property '{propertyName}'.");
        }
    }
}
