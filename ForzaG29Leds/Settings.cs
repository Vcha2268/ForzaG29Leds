using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ForzaG29Leds;

public sealed class Settings
{
    public int Port { get; set; } = 9999;
    public int SolidPercent { get; set; } = 77;
    public int FlashPercent { get; set; } = 82;
    public int FlashIntervalMs { get; set; } = 80;

    [JsonIgnore]
    public float SolidRatio => SolidPercent / 100f;
    [JsonIgnore]
    public float FlashRatio => FlashPercent / 100f;

    private static readonly string FilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "ForzaG29Leds", "settings.json");

    private static readonly JsonSerializerOptions JsonOpts =
        new() { WriteIndented = true };

    public static Settings Load()
    {
        try
        {
            if (File.Exists(FilePath))
                return JsonSerializer.Deserialize<Settings>(File.ReadAllText(FilePath)) ?? new();
        }
        catch { }
        return new();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            File.WriteAllText(FilePath, JsonSerializer.Serialize(this, JsonOpts));
        }
        catch { }
    }
}
