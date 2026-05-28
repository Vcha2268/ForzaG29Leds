using System.Text.Json;
using Xunit;

namespace ForzaG29Leds.Tests;

public class SettingsTests
{
    [Fact]
    public void Defaults_AreCorrect()
    {
        var s = new Settings();
        Assert.Equal(9999, s.Port);
        Assert.Equal(77, s.SolidPercent);
        Assert.Equal(82, s.FlashPercent);
        Assert.Equal(80, s.FlashIntervalMs);
    }

    [Fact]
    public void SolidRatio_IsSolidPercentDividedBy100()
    {
        var s = new Settings { SolidPercent = 85 };
        Assert.Equal(0.85f, s.SolidRatio);
    }

    [Fact]
    public void FlashRatio_IsFlashPercentDividedBy100()
    {
        var s = new Settings { FlashPercent = 90 };
        Assert.Equal(0.90f, s.FlashRatio);
    }

    [Fact]
    public void JsonRoundTrip_PreservesAllValues()
    {
        var original = new Settings
        {
            Port = 12345,
            SolidPercent = 75,
            FlashPercent = 88,
            FlashIntervalMs = 120,
        };

        string json = JsonSerializer.Serialize(original);
        var restored = JsonSerializer.Deserialize<Settings>(json)!;

        Assert.Equal(original.Port, restored.Port);
        Assert.Equal(original.SolidPercent, restored.SolidPercent);
        Assert.Equal(original.FlashPercent, restored.FlashPercent);
        Assert.Equal(original.FlashIntervalMs, restored.FlashIntervalMs);
    }

    [Fact]
    public void JsonRoundTrip_MissingFieldsFallBackToDefaults()
    {
        // Deserialising an empty object should give default values
        var s = JsonSerializer.Deserialize<Settings>("{}")!;
        Assert.Equal(new Settings().Port, s.Port);
        Assert.Equal(new Settings().SolidPercent, s.SolidPercent);
        Assert.Equal(new Settings().FlashPercent, s.FlashPercent);
        Assert.Equal(new Settings().FlashIntervalMs, s.FlashIntervalMs);
    }
}
