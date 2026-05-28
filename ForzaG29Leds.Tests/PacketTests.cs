using System.Runtime.InteropServices;
using Xunit;

namespace ForzaG29Leds.Tests;

public class PacketTests
{
    [Fact]
    public void Struct_SizeIs324Bytes()
    {
        Assert.Equal(324, Marshal.SizeOf<ForzaTelemetryPacket>());
    }

    // ── Sled block offsets ────────────────────────────────────────────────────

    [Theory]
    [InlineData(nameof(ForzaTelemetryPacket.IsRaceOn), 0)]
    [InlineData(nameof(ForzaTelemetryPacket.TimestampMS), 4)]
    [InlineData(nameof(ForzaTelemetryPacket.EngineMaxRpm), 8)]
    [InlineData(nameof(ForzaTelemetryPacket.EngineIdleRpm), 12)]
    [InlineData(nameof(ForzaTelemetryPacket.CurrentEngineRpm), 16)]
    [InlineData(nameof(ForzaTelemetryPacket.CarOrdinal), 212)]
    [InlineData(nameof(ForzaTelemetryPacket.NumCylinders), 228)]
    public void SledField_HasCorrectOffset(string fieldName, int expectedOffset)
    {
        int actual = (int)Marshal.OffsetOf<ForzaTelemetryPacket>(fieldName);
        Assert.Equal(expectedOffset, actual);
    }

    // ── Car Dash extension offsets ────────────────────────────────────────────

    [Theory]
    [InlineData(nameof(ForzaTelemetryPacket.PositionX), 244)]
    [InlineData(nameof(ForzaTelemetryPacket.PositionY), 248)]
    [InlineData(nameof(ForzaTelemetryPacket.PositionZ), 252)]
    [InlineData(nameof(ForzaTelemetryPacket.Speed), 256)]
    [InlineData(nameof(ForzaTelemetryPacket.Power), 260)]
    [InlineData(nameof(ForzaTelemetryPacket.Torque), 264)]
    [InlineData(nameof(ForzaTelemetryPacket.TireTempFlF), 268)]
    [InlineData(nameof(ForzaTelemetryPacket.TireTempFrF), 272)]
    [InlineData(nameof(ForzaTelemetryPacket.TireTempRlF), 276)]
    [InlineData(nameof(ForzaTelemetryPacket.TireTempRrF), 280)]
    [InlineData(nameof(ForzaTelemetryPacket.Boost), 284)]
    [InlineData(nameof(ForzaTelemetryPacket.Fuel), 288)]
    [InlineData(nameof(ForzaTelemetryPacket.BestLap), 296)]
    [InlineData(nameof(ForzaTelemetryPacket.LastLap), 300)]
    [InlineData(nameof(ForzaTelemetryPacket.CurrentLap), 304)]
    [InlineData(nameof(ForzaTelemetryPacket.LapNumber), 312)]
    [InlineData(nameof(ForzaTelemetryPacket.RacePosition), 314)]
    [InlineData(nameof(ForzaTelemetryPacket.Accel), 315)]
    [InlineData(nameof(ForzaTelemetryPacket.Brake), 316)]
    [InlineData(nameof(ForzaTelemetryPacket.Gear), 319)]
    [InlineData(nameof(ForzaTelemetryPacket.Steer), 320)]
    [InlineData(nameof(ForzaTelemetryPacket.TrackOrdinal), 323)]
    public void DashField_HasCorrectOffset(string fieldName, int expectedOffset)
    {
        int actual = (int)Marshal.OffsetOf<ForzaTelemetryPacket>(fieldName);
        Assert.Equal(expectedOffset, actual);
    }

    // ── Fahrenheit → Celsius helpers ──────────────────────────────────────────

    [Theory]
    [InlineData(32f, 0f)]
    [InlineData(212f, 100f)]
    [InlineData(98.6f, 37f)]
    [InlineData(-40f, -40f)]   // F == C at -40
    public void TireTempFlC_ConvertsFromFahrenheit(float fahrenheit, float expectedCelsius)
    {
        var pkt = new ForzaTelemetryPacket { TireTempFlF = fahrenheit };
        Assert.Equal(expectedCelsius, pkt.TireTempFlC, precision: 1);
    }

    [Fact]
    public void AllTireTempHelpers_UseIndependentFields()
    {
        var pkt = new ForzaTelemetryPacket
        {
            TireTempFlF = 32f,
            TireTempFrF = 212f,
            TireTempRlF = 98.6f,
            TireTempRrF = -40f,
        };
        Assert.Equal(0f, pkt.TireTempFlC, precision: 1);
        Assert.Equal(100f, pkt.TireTempFrC, precision: 1);
        Assert.Equal(37f, pkt.TireTempRlC, precision: 1);
        Assert.Equal(-40f, pkt.TireTempRrC, precision: 1);
    }
}
